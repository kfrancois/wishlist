using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.Storage.Streams;
using Windows.Security.Credentials;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace WishList.Services
{
    enum RequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    class ApiService
    {
        private readonly string baseUrl = "http://localhost:60855/api";

        private HttpClient _httpClient;
        private PasswordVault _vault;

        public static ApiService Instance { get; } = new ApiService();

        public string Token { // Gets the token from the vault
            get {
                var token = _vault.FindAllByResource("token").FirstOrDefault();
                token.RetrievePassword();
                return token.Password;
            }
            set { // Sets the token and adds or removes it from the default headers
                var login = _vault.FindAllByResource("login").FirstOrDefault();
                _vault.Add(new PasswordCredential("token", login.UserName, value));

                if (String.IsNullOrEmpty(value))
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {value}");
                }
            }
        }

        private ApiService()
        {
            _vault = new PasswordVault();
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new HttpContentCodingWithQualityHeaderValue("utf-8"));
        }

        public async Task<HttpResponseMessage> SendRequest(RequestType requestType, string endpoint, Object body = null, bool retry = true)
        {
            HttpResponseMessage httpResponse;

            try
            {
                switch (requestType)
                {
                    case RequestType.GET:
                        httpResponse = await _httpClient.GetAsync(new Uri($"{baseUrl}/{endpoint}"));
                        break;
                    case RequestType.POST:
                        httpResponse = await _httpClient.PostAsync(new Uri($"{baseUrl}/{endpoint}"), new HttpStringContent(JsonConvert.SerializeObject(body), UnicodeEncoding.Utf8, "application/json"));
                        break;
                    case RequestType.PUT:
                        httpResponse = await _httpClient.PutAsync(new Uri($"{baseUrl}/{endpoint}"), new HttpStringContent(JsonConvert.SerializeObject(body), UnicodeEncoding.Utf8, "application/json"));
                        break;
                    case RequestType.DELETE:
                        httpResponse = await _httpClient.DeleteAsync(new Uri($"{baseUrl}/{endpoint}"));
                        break;
                    default: throw new Exception("Need a RequestType");
                }

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized) // If token expired, fetch token and retry request once
                {
                    await FetchToken();
                    return await SendRequest(requestType, endpoint, body, false);
                }
                return httpResponse;
            }
            catch (Exception e)
            {
                var serverIsDown = e.Message.Contains("A connection with the server could not be established");
                await new MessageDialog("No connection could be made to the server. Please try again later.", "Connection error").ShowAsync();
                Application.Current.Exit();
                return null;
            }
        }

        public async Task FetchToken()
        {
            var login = _vault.FindAllByResource("login").FirstOrDefault();
            login.RetrievePassword();

            var response = await SendRequest(RequestType.POST, "account/login", new { Email = login.UserName, login.Password });
            var content = await GetContentFromResponse(response);
            Token = JsonConvert.DeserializeObject<Token>(content).Value;
        }

        public async Task<bool> TokenCheck()
        {
            var response = await SendRequest(RequestType.POST, "account/tokencheck", new { });

            return response.StatusCode != HttpStatusCode.Unauthorized;
        }

        public async Task Register(string username, string password)
        {
            var response = await SendRequest(RequestType.POST, "account/register", new { Email = username, Password = password });
            var content = await GetContentFromResponse(response);

            _vault.Add(new PasswordCredential("login", username, password));
            Token = JsonConvert.DeserializeObject<Token>(content).Value; // Token is returned on register call
        }

        public async Task SaveLoginDetails(string username, string password)
        {
            try
            {
                _vault.Add(new PasswordCredential("login", username, password));
            }
            catch (ArgumentException)
            {
                RemoveCredential("login");
                _vault.Add(new PasswordCredential("login", username, password));
            }
            await FetchToken(); // When login in again, new token is necessary
        }

        public void LogOut()
        {
            RemoveCredential("login");
            RemoveCredential("token");
            Application.Current.Exit();
        }

        private void RemoveCredential(string key)
        {
            try // Only credential if it exists, ignore exception
            {
                var credential = _vault.FindAllByResource(key).FirstOrDefault();
                credential.RetrievePassword();
                _vault.Remove(new PasswordCredential(key, credential.UserName, credential.Password));
            }
            catch (Exception) { }
        }



        public bool IsSaved(string resource)
        {
            try
            {
                return _vault.FindAllByResource(resource).Count > 0;
            }
            catch (Exception) // Vault throws exception when resource not found
            {
                return false;
            }
        }

        public async Task<string> GetContentFromResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public string GetUser()
        {
            var login = _vault.FindAllByResource("login").FirstOrDefault();
            return login.UserName;
        }
    }

    class Token
    {
        [JsonProperty("token")]
        public string Value { get; set; }
        [JsonProperty("expiresAt")]
        public DateTime ExpiresAt { get; set; }
    }
}

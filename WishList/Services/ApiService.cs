using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.Storage.Streams;
using Windows.Security.Credentials;
using System.Linq;

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

        public async Task FetchToken()
        {
            var login = _vault.FindAllByResource("login").FirstOrDefault();
            login.RetrievePassword();

            var response = await SendRequest(RequestType.POST, "account/login", new { Email = login.UserName, login.Password });
            var content = await GetContentFromResponse(response);
            Token = JsonConvert.DeserializeAnonymousType(content, new { token = "" }).token;
        }

        public async Task<bool> TokenCheck()
        {
            var response = await SendRequest(RequestType.POST, "account/tokencheck", new { });

            return response.StatusCode != HttpStatusCode.Unauthorized;
        }

        public void SaveLoginDetails(string username, string password)
        {
            _vault.Add(new PasswordCredential("login", username, password));

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
}

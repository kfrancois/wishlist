using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Newtonsoft.Json;
using WishList.Model;

namespace WishList.Services
{
    class WishListService {
        private string baseUrl = "http://localhost:60855/";

        private string _token;

        public void AuthenticateUser(string email, string password) {

            var client = new HttpClient {BaseAddress = new Uri(baseUrl)};

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(new {
                Email = email,
                Password = password
            }), Encoding.UTF8, "application/json");

           var response = client.PostAsync("account/login", content);

            var jsonResponse = response.Result.Content.ReadAsStringAsync();

            _token = jsonResponse.Result;

        }

        public IEnumerable<Wishlist> GetWishLists() {
            var client = new HttpClient { BaseAddress = new Uri(baseUrl) };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = client.GetAsync("api/wishlist");

            return JsonConvert.DeserializeObject<IEnumerable<Wishlist>>(response.Result.Content.ReadAsStringAsync().Result);
        }
    }
}

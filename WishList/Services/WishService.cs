using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Model;

namespace WishList.Services
{
    public class WishService
    {
        public static WishService Instance { get; } = new WishService();

        private static readonly string _urlExtension = "wish";

        private ApiService _apiService;
        public WishService() => _apiService = ApiService.Instance;

        public async Task<List<Wish>> GetAllWishes()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/all"));
            return JsonConvert.DeserializeObject<List<Wish>>(request.Result);
        }

        public async Task<Wish> GetWish(int id)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/{id}"));
            return JsonConvert.DeserializeObject<Wish>(request.Result);
        }

        public async Task<Wish> CreateWish(Wish wish)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}", JsonConvert.SerializeObject(wish)));
            return JsonConvert.DeserializeObject<Wish>(request.Result);
        }

        public async Task<Wish> PutWish(int id, Wish wish)
        {
            var jsonWish = JsonConvert.SerializeObject(wish);
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.PUT, $"{_urlExtension}/{id}", jsonWish));
            return JsonConvert.DeserializeObject<Wish>(request.Result);
        }

        public async Task DeleteWish(Wish wish)
        {
            await _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.DELETE, $"{_urlExtension}/{wish.WishId}"));
        }
    }
}

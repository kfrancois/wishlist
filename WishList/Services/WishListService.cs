using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Model;

namespace WishList.Services
{
    public class WishListService
    {
        public static WishListService Instance { get; } = new WishListService();

        private static readonly string _urlExtension = "wishlist";

        private ApiService _apiService;
        public WishListService() => _apiService = ApiService.Instance;

        public async Task<List<Wishlist>> GetWishlists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, _urlExtension));
            return JsonConvert.DeserializeObject<List<Wishlist>>(request.Result);
        }

        public async Task<List<Wishlist>> GetWishlist(int id)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/{id}"));
            return JsonConvert.DeserializeObject<List<Wishlist>>(request.Result);
        }

        public async Task<Wishlist> CreateWishlist(Wishlist wishlist)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}", JsonConvert.SerializeObject(wishlist)));
            return JsonConvert.DeserializeObject<Wishlist>(request.Result);
        }

        public async Task DeleteWishlist(Wishlist wishlist)
        {
            await _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.DELETE, $"{_urlExtension}/{wishlist.WishlistId}"));
        }

        public async Task<Wish> CreateWish(int wishlistId, Wish wish)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}", JsonConvert.SerializeObject(wish)));
            return JsonConvert.DeserializeObject<Wish>(request.Result);
        }

        public async Task<List<Wishlist>> SubscribedWishLists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/subscribed"));
            return JsonConvert.DeserializeObject<List<Wishlist>>(request.Result);
        }

        public async Task<List<Wishlist>> InvitedWishLists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/invited"));
            return JsonConvert.DeserializeObject<List<Wishlist>>(request.Result);
        }

        public async Task<string> InvitePerson(int wishlistId, string email)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}/invite", JsonConvert.SerializeObject(new { Email = email })));
            return JsonConvert.DeserializeObject<string>(request.Result);
        }

        public async Task<bool> AcceptInvite(int wishlistId)
        {
            var request = await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}/accept");
            return request.IsSuccessStatusCode;
        }
    }
}

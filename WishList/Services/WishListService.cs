using Newtonsoft.Json;
using System.Collections.ObjectModel;
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

        public async Task<ObservableCollection<Wishlist>> GetAllWishlists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/all"));
            return JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(request.Result);
        }

        public async Task<ObservableCollection<Wishlist>> GetWishlists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, _urlExtension));
            return JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(request.Result);
        }

        public async Task<ObservableCollection<Wishlist>> BrowseWishlists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/browse"));
            return JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(request.Result);
        }

        public async Task<Wishlist> GetWishlist(int id)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/{id}"));
            return JsonConvert.DeserializeObject<Wishlist>(request.Result);
        }

        public async Task<Wishlist> CreateWishlist(Wishlist wishlist)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}", wishlist));
            return JsonConvert.DeserializeObject<Wishlist>(request.Result);
        }

        public async Task DeleteWishlist(Wishlist wishlist)
        {
            await _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.DELETE, $"{_urlExtension}/{wishlist.WishlistId}"));
        }

        public async Task<Wish> CreateWish(int wishlistId, Wish wish)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}", wish));
            return JsonConvert.DeserializeObject<Wish>(request.Result);
        }

        public async Task<ObservableCollection<Wishlist>> SubscribedWishLists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/subscribed"));
            return JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(request.Result);
        }

        public async Task<ObservableCollection<Wishlist>> InvitedWishLists()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.GET, $"{_urlExtension}/invited"));
            return JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(request.Result);
        }

        public async Task<string> InvitePerson(int wishlistId, string email)
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}/invite", new { Email = email }));
            return JsonConvert.DeserializeObject<string>(request.Result);
        }

        public async Task<bool> AcceptInvite(int wishlistId)
        {
            var request = await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}/accept");
            return request.IsSuccessStatusCode;
        }

        public async Task<ObservableCollection<PendingRequest>> GetRequests()
        {
            var request = _apiService.GetContentFromResponse(await _apiService.SendRequest(RequestType.POST, "{_urlExtension}/requests"));
            return JsonConvert.DeserializeObject<ObservableCollection<PendingRequest>>(request.Result);
        }

        public async Task<bool> RequestAccess(int wishlistId)
        {
            var request = await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}/request");
            return request.IsSuccessStatusCode;
        }

        public async Task<bool> GrantAccess(int wishlistId, string email)
        {
            var request = await _apiService.SendRequest(RequestType.POST, $"{_urlExtension}/{wishlistId}/grant", new { Email = email });
            return request.IsSuccessStatusCode;
        }
    }
}

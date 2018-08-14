using Newtonsoft.Json;
using System.Collections.Generic;

namespace WishList.Model
{
    public class User
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        public List<Wishlist> SubscribedLists { get; set; }

        public List<Wishlist> Wishlists { get; set; }

        public List<PendingInvite> Invites { get; set; }

        public User()
        {
        }

        public void AddWishlist(Wishlist wishlist)
        {
            Wishlists.Add(wishlist);
        }

        public void RemoveWishlist(Wishlist wishlist)
        {
            Wishlists.Remove(wishlist);
        }
        public void AddWish(Wish wish, Wishlist wishlist)
        {
            wishlist.AddWish(wish);
        }
        public void AddWishlist(Wish wish, Wishlist wishlist)
        {
            wishlist.RemoveWish(wish);
        }
    }
}

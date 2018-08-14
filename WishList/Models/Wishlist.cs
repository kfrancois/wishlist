using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Model
{
    public class Wishlist
    {
        [JsonProperty("wishListId")]
        public int WishlistId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("wishes")]
        public List<Wish> Wishes { get; set; }
        [JsonProperty("pendingInvites")]
        public List<PendingInvite> Invites { get; set; }
        /*[JsonProperty("subscribers")]
        public List<User> Subscribers { get; set; }*/
        [JsonProperty("pendingRequests")]
        public List<PendingRequest> Requests { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("creatorName")]
        public string CreatorName { get; set; }
        public Wishlist() { }

        public Wishlist(string title)
        {
            Title = title;
            Description = null;
            Wishes = new List<Wish>();
            Invites = new List<PendingInvite>();
            Requests = new List<PendingRequest>();
            //Subscribers = new List<User>();
        }

        public void AddWish(Wish wish)
        {
            Wishes.Add(wish);
        }
        public void RemoveWish(Wish wish)
        {
            Wishes.Remove(wish);
        }
    }
}

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
        public DateTime Date { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("wishes")]
        public List<Wish> Wishes { get; private set; }
        [JsonProperty("pendingInvites")]
        public List<PendingInvite> Invites { get; private set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        public Wishlist() { }

        public void AddWish(Wish wish) {
            Wishes.Add(wish); 
        }
        public void RemoveWish(Wish wish) {
            Wishes.Remove(wish); 
        }
    }
}

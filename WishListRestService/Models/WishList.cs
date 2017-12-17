using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class WishList
    {
        public int WishListId { get; set; }
        public string Title { get; set; }
        public string CreatorName { get; set; }
        public List<Wish> Wishes { get; set; }
        public List<WishListSubscriber> Subscribers { get; set; }
        public List<PendingInvite> PendingInvites { get; set; }

        public WishList()
        {
            Wishes = new List<Wish>();
            Subscribers = new List<WishListSubscriber>();
            PendingInvites = new List<PendingInvite>();
        }

        public void AddWish(Wish item)
        {
            Wishes.Add(item);
        }

        public void AddInvite(PendingInvite invite)
        {
            PendingInvites.Add(invite);
            invite.User.AddInvite(invite);
        }
    }
}
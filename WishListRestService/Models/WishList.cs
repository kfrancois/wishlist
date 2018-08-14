using System;
using System.Collections.Generic;

namespace WishListRestService.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public List<Wish> Wishes { get; set; }
        public List<WishlistSubscriber> Subscribers { get; set; }
        public List<PendingInvite> PendingInvites { get; set; }
        public List<PendingRequest> PendingRequests { get; set; }

        public Wishlist()
        {
            Wishes = new List<Wish>();
            Subscribers = new List<WishlistSubscriber>();
            PendingInvites = new List<PendingInvite>();
            PendingRequests= new List<PendingRequest>();
        }

        public Wishlist(string title) : this()
        {
            Title = title;
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

        public void AddSubscriber(WishlistSubscriber subscriber)
        {
            Subscribers.Add(subscriber);
            subscriber.User.Subscribe(subscriber);
        }

        public void AddRequest(PendingRequest request)
        {
            PendingRequests.Add(request);
            request.User.AddRequest(request);
        }
    }
}
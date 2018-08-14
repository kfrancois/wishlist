using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WishListRestService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Wishlist> CreatedWishLists { get; set; }
        public ICollection<PendingInvite> PendingInvites { get; set; }
        public ICollection<PendingRequest> PendingRequests { get; set; }
        public ICollection<WishlistSubscriber> SubbedWishLists { get; set; }

        public ApplicationUser()
        {
            PendingInvites = new List<PendingInvite>();
            PendingRequests = new List<PendingRequest>();
            CreatedWishLists = new List<Wishlist>();
            SubbedWishLists = new List<WishlistSubscriber>();
        }

        public ApplicationUser(ICollection<PendingInvite> pendingInvites, ICollection<Wishlist> createdWishLists,
            ICollection<WishlistSubscriber> subbedWishLists) : this()
        {
            PendingInvites = pendingInvites;
            CreatedWishLists = createdWishLists;
            SubbedWishLists = subbedWishLists;
        }

        public void CreateWishList(Wishlist wishList)
        {
            CreatedWishLists.Add(wishList);
            wishList.CreatorName = UserName;
        }

        public void AddInvite(PendingInvite invite)
        {
            PendingInvites.Add(invite);
        }

        public void AddRequest(PendingRequest request)
        {
            PendingRequests.Add(request);
        }

        public void Subscribe(WishlistSubscriber subscriber)
        {
            SubbedWishLists.Add(subscriber);
        }
    }
}
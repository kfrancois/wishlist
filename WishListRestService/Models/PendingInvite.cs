namespace WishListRestService.Models
{
    public class PendingInvite
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int WishListId { get; set; }
        public Wishlist WishList { get; set; }
    }
}
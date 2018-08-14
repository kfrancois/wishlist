namespace WishListRestService.Models
{
    public class PendingRequest
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int WishListId { get; set; }
        public Wishlist WishList { get; set; }
    }
}
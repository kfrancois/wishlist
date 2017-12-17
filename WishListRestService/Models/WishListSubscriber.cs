namespace WebApplication1.Models
{
    public class WishListSubscriber
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int WishListId { get; set; }
        public WishList WishList { get; set; }
    }
}
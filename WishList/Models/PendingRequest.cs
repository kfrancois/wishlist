namespace WishList.Model
{
    public class PendingRequest
    {

        public Wishlist Wishlist { get; set; }

        public User User { get; set; }

        public PendingRequest(Wishlist wishlist, User user)
        {
            Wishlist = wishlist;
            User = user;
        }
    }
}

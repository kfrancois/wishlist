using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Model
{
   public class User
    {
        private List<Wishlist> subscribedLists;
        private List<Wishlist> wishlists;
        private string firstName;
        private string lastName;
        private List<PendingInvite> invites;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public List<Wishlist> SubscribedLists
        {
            get { return subscribedLists; }
            set { subscribedLists = value; }
        }

        public List<Wishlist> Wishlists
        {
            get { return wishlists; }
            set { wishlists = value; }
        }
        //picture 
      
        public List<PendingInvite> Invites
        {
            get { return invites; }
            set { invites = value; }
        }

        public User()
        {

        }
        public Wishlist invitePerson()
        {
            return null;
        }

        public Wishlist requestJoin()
        {
            return null;
        }

        public void addWishlist(Wishlist wishlist)
        {
            this.wishlists.Add(wishlist); 
        }

        public void removeWishlist(Wishlist wishlist)
        {
            this.wishlists.Remove(wishlist); 
        }
        public void addWish(Wish wish, Wishlist wishlist)
        {
            wishlist.addWish(wish); 
        }
        public void addWishlist(Wish wish, Wishlist wishlist)
        {
            wishlist.removeWish(wish); 
        }
    }
}

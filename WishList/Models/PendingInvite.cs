using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Model
{
    public class PendingInvite
    {
        private Wishlist wishlist;
        private User toUser;

        public Wishlist Wishlist {
            get {
                return wishlist;
            }
            set {
                wishlist = value;
            }
        }

        public User User {
            get { return toUser; }
            set { toUser = value; }
        }

        public PendingInvite(Wishlist wishlist, User toUser)
        {
            this.wishlist = wishlist;
            this.toUser = toUser;
        }
    }
}

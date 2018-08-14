using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishList.Model;

namespace WishList.Models
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

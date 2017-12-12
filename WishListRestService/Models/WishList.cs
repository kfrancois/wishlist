using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WishListRestService.Models {
    public class WishList {
        public int WishListId { get; private set; }
        public string Title { get; set; }
        public ApplicationUser Creator { get; set; }
        public List<Wish> Wishes { get; set; }
        public List<ApplicationUser> PendingInvites { get; set; }
    }
}
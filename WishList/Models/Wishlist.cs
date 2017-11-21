using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Model
{
    class Wishlist
    {
        private DateTime date;
        private string title;
        private string description;
        private List<Wish> wishes;
        private List<PendingInvite> invites;


        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public List<Wish> Wishes
        {
            get { return wishes; }
            private set { wishes = value; }
        }
        public List<PendingInvite> Invites
        {
            get { return invites; }
            private set { invites = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public Wishlist() { }

        public void addWish(Wish wish) {
            this.wishes.Add(wish); 
        }
        public void removeWish(Wish wish) {
            this.wishes.Remove(wish); 
        }
    }
}

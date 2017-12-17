using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using WishList.Model;

namespace WishList.ViewModels
{
    public class WishListOverviewPageViewModel : ViewModelBase
    {
        public string Name { get; private set; }
        private List<Wish> wishListItem = new List<Wish>();
        public List<Wish> WishListItem { get => wishListItem; set => wishListItem = value; }
        public List<Wishlist> wishList = new List<Wishlist>();
        public List<Wishlist> WishList { get => wishList; set => wishList = value; }

        public WishListOverviewPageViewModel()
        {
            MakeHardcodeWishes();
            MakeHardcodeWishLists();
        }

        private void MakeHardcodeWishes()
        {
            for (int i = 0; i < 10; i++)
            {
                this.WishListItem.Add(new Wish(new Category("Birthday"), "Title" + (i + 1).ToString(), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Mauris at eleifend augue.Cras mattis, nisi id aliquam ornare, magna leo elementum arcu, ut porttitor mi metus eget ligula.", (i + 1) * 2.46));
            }
        }

        private void MakeHardcodeWishLists()
        {
            for (int i = 0; i < 10; i++)
            {
                Wishlist list = new Wishlist();
                list.Title = "Title" + (i + 1).ToString();
                list.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Mauris at eleifend augue.Cras mattis, nisi id aliquam ornare, magna leo elementum arcu, ut porttitor mi metus eget ligula.";
                this.WishList.Add(list);
            }
        }
    }
}

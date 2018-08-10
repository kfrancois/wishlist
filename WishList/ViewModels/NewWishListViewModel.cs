using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using WishList.Model;
using WishList.Services;

namespace WishList.ViewModels
{
    public class NewWishListViewModel : ViewModelBase
    {

        private WishListService wishListService;
        private string _name;

        public string WishListName {
            get {
                return _name;
            }
            set {
                if (value == _name)
                    return;

                _name = value;
            }
        }

        public NewWishListViewModel()
        {
            wishListService = WishListService.Instance;
        }

        public async Task SaveAsync()
        {
            Wishlist newList = new Wishlist(WishListName);
            newList.CreatorName = null;
            await wishListService.CreateWishlist(newList);
            //GoBack();
        }

        public void GoBack() => NavigationService.GoBack();
    }
}

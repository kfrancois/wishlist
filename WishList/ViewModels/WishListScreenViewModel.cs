using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.ViewModels
{
    public class WishListScreenViewModel : ViewModelBase
    {
        public string Name { get; private set; }

        public ObservableCollection<Wishlist> WishLists { get; set; }

        private WishListService wishListService;
        private Wishlist _selectedItem;

        public WishListScreenViewModel()
        {
            wishListService = WishListService.Instance;
        }
        
        public void GotoNewWishList() =>
            App.Current.NavigationService.Navigate(typeof(Views.NewWishList));
    }
}

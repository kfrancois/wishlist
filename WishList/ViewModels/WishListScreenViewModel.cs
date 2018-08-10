using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using WishList.Model;
using WishList.Services;

namespace WishList.ViewModels
{
    public class WishListScreenViewModel : ViewModelBase
    {
        public string Name { get; private set; }

        public ObservableCollection<Wishlist> WishLists { get; set; }

        public Wishlist SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                if (value == _selectedItem)
                    return;

                _selectedItem = value;

                System.Diagnostics.Debug.WriteLine(_selectedItem.Title);

                NavigationService.Navigate(typeof(Views.WishListDetailPage), (int) _selectedItem.WishlistId);
            }
        }
        private WishListService wishListService;
        private Wishlist _selectedItem;

        public WishListScreenViewModel()
        {
            wishListService = WishListService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            ObservableCollection<Wishlist> ret = await wishListService.GetWishlists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;
            
            System.Diagnostics.Debug.WriteLine("Collection: " + WishLists.First().Title);
        }

        public void GotoNewWishList() =>
            App.Current.NavigationService.Navigate(typeof(Views.NewWishList));
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.ViewModels
{
    public class SubscriptionsPageViewModel : ViewModelBase
    {
        public string Name { get; private set; }
        public ObservableCollection<Wishlist> WishLists { get; set; }
        private WishListService wishListService;

        public SubscriptionsPageViewModel()
        {
            wishListService = WishListService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            ObservableCollection<Wishlist> ret = await wishListService.SubscribedWishLists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            System.Diagnostics.Debug.WriteLine(ret.Count());
        }

        public void GoBack() => NavigationService.Navigate(typeof(Views.MainPage));
    }
}

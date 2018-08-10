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
    public class NewInviteViewModel : ViewModelBase
    {

        public ObservableCollection<Wishlist> WishLists { get; set; }
        public ObservableCollection<string> WishListNames { get; set; }
        public string WishList { get; set; }
        public string Username { get; set; }
        private WishListService wishListService;

        public NewInviteViewModel()
        {
            wishListService = WishListService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            ObservableCollection<Wishlist> ret = await wishListService.GetWishlists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            WishListNames = new ObservableCollection<string>();

            foreach (Wishlist w in WishLists)
                WishListNames.Add(w.Title);
        }

        public async void Send()
        {
            int SelectedWishList = WishLists.First(wl => wl.Title == WishList).WishlistId;
            await wishListService.InvitePerson(SelectedWishList, Username);
        }

        public void GoBack() => NavigationService.GoBack();
    }
}

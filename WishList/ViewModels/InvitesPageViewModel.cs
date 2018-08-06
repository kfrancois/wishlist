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
    public class InvitesPageViewModel : ViewModelBase
    {

        private WishListService wishListService;
        public ObservableCollection<Wishlist> WishLists { get; set; }

        private Wishlist _selectedItem;

        public Wishlist SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                if (value == _selectedItem)
                    return;

                _selectedItem = value;

                ShowDialog(_selectedItem);

            }
        }

        private async void ShowDialog(Wishlist item)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Are you sure you want to accept " + item.Title + " from " + item.CreatorName + "?",
                        "Accept this invite");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            System.Diagnostics.Debug.WriteLine(result.Id);
        }

        public InvitesPageViewModel()
        {
            wishListService = WishListService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            ObservableCollection<Wishlist> ret = await wishListService.InvitedWishLists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

        }

        public void GotoNewInvites() => NavigationService.Navigate(typeof(Views.NewInvite));

        public void GoBack() => NavigationService.Navigate(typeof(Views.MainPage));
    }
}

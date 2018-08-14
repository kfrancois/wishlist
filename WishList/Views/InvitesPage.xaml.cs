using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.Views
{

    public sealed partial class InvitesPage : Page
    {
        public ObservableCollection<Wishlist> WishLists { get; set; }
        public ObservableCollection<PendingRequest> Requests { get; set; }
        private WishListService wishListService;

        public InvitesPage()
        {
            this.InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret = await wishListService.InvitedWishLists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            ListView1.ItemsSource = WishLists;

            ObservableCollection<PendingRequest> ret1 = await wishListService.GetRequests();

            Requests = ret1.Count() == 0 ? new ObservableCollection<PendingRequest>() : ret1;

            ListView2.ItemsSource = Requests;
        }

        private async void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (Wishlist)ListView1.SelectedItem;

            if (item == null) return;

            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Are you sure you want to accept wishlist \"" + item.Title + "\" from " + item.CreatorName + "?",
                        "Accept invite");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (result.Id.ToString() == "0")
            {
                AcceptInvite(item);
            }

            System.Diagnostics.Debug.WriteLine(result.Id);
        }

        private async void AcceptInvite(Wishlist item)
        {
            await wishListService.AcceptInvite(item.WishlistId);
            WishLists.Remove(item);
        }

        public void NewInvite(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(NewInvite));
        }

        private async void ListView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (PendingRequest)ListView2.SelectedItem;

            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Do you want to grant access?",
                        "Accept Request");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (result.Id.ToString() == "0")
            {
                AcceptRequest(item);
            }
        }

        private async void AcceptRequest(PendingRequest item)
        {
            int wishlistId = item.Wishlist.WishlistId;
            string user = item.User.UserName;
            await wishListService.GrantAccess(wishlistId, user);
        }
    }
}

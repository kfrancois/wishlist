using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.Views
{

    public sealed partial class InvitesPage : Page
    {
        public ObservableCollection<Wishlist> Invites { get; set; }
        public ObservableCollection<PendingRequest> Requests { get; set; }
        private WishListService wishListService;

        public InvitesPage()
        {
            InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Invites = await wishListService.InvitedWishLists();
            InvitesListBox.ItemsSource = Invites.Count() == 0 ? new ObservableCollection<Wishlist>() : Invites;

            Requests = await wishListService.GetRequests();
            RequestsListBox.ItemsSource = Requests.Count() == 0 ? new ObservableCollection<PendingRequest>() : Requests;
        }

        private async void InviteSelected(object sender, SelectionChangedEventArgs e)
        {
            var item = (Wishlist)InvitesListBox.SelectedItem;

            if (item == null) return;


            var text = $"Are you sure you want to accept wishlist '{item.Title}' from {item.CreatorName} ?";
            var title = "Accept invite";

            if (await ShowDialog(text, title))
            {
                AcceptInvite(item);
            }
            else
            {
                InvitesListBox.SelectedItem = null;
            }
        }

        private async void AcceptInvite(Wishlist item)
        {
            await wishListService.AcceptInvite(item.WishlistId);
            Invites.Remove(item);
        }

        public void NewInvite(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(NewInvite));
        }

        private async void RequestSelected(object sender, SelectionChangedEventArgs e)
        {
            var item = (PendingRequest)RequestsListBox.SelectedItem;

            if (item == null) return;

            var text = "Do you want to grant access?";
            var title = "Accept request";

            if (await ShowDialog(text, title))
            {
                AcceptRequest(item);
            }
            else
            {
                RequestsListBox.SelectedItem = null;
            }
        }

        private async void AcceptRequest(PendingRequest item)
        {
            await wishListService.GrantAccess(item.Wishlist.WishlistId, item.User.UserName);
            Requests.Remove(item);
        }

        private async Task<bool> ShowDialog(string text, string title)
        {
            var dialog = new MessageDialog(text, title);

            dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();
            return result.Id.ToString() == "0";
        }
    }
}

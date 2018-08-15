using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.Views
{
    public sealed partial class Browse : Page
    {

        private WishListService wishListService;

        public ObservableCollection<Wishlist> WishLists { get; set; }

        public Browse()
        {
            InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret1 = await wishListService.BrowseWishlists();

            WishLists = ret1.Count() == 0 ? new ObservableCollection<Wishlist>() : ret1;

            ListBox.ItemsSource = WishLists;

        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedList = (Wishlist)ListBox.SelectedItem;

            if (SelectedList == null) return;

            var text = "Do you want to enter this wishlist?";
            var title = "Send Request";

            if (await ShowDialog(text, title))
            {
                await wishListService.RequestAccess(SelectedList.WishlistId);
                WishLists.Remove(SelectedList);
            }
            else
            {
                ListBox.SelectedItem = null;
            }
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
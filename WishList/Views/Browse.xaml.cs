using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WishList.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Browse : Page
    {

        private WishListService wishListService;

        public ObservableCollection<Wishlist> WishLists { get; set; }

        public Browse()
        {
            this.InitializeComponent();
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

            var dialog = new Windows.UI.Popups.MessageDialog(
                            "Do you want to enter this wishlist?",
                            "Send Request");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                await wishListService.RequestAccess(SelectedList.WishlistId);
            }

        }

    }

}
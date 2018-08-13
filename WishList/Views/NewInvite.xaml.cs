using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
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
    public sealed partial class NewInvite : Page
    {

        public ObservableCollection<Wishlist> WishLists { get; set; }
        public ObservableCollection<string> WishListNames { get; set; }
        private WishListService wishListService;

        public NewInvite()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret = await wishListService.GetWishlists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            WishListNames = new ObservableCollection<string>();

            foreach (Wishlist w in WishLists)
                WishListNames.Add(w.Title);

            wishlistbox.ItemsSource = WishListNames;
        }

       // public async void Send(object sender, RoutedEventArgs e)
        public void Send(object sender, RoutedEventArgs e)
        {
            /*int SelectedWishList = WishLists.First(wl => wl.Title == WishList).WishlistId;
            await wishListService.InvitePerson(SelectedWishList, Username);*/

            Frame.GoBack();
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

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
using WishList.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WishList.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SubscriptionsPage : Page
    {
        public ObservableCollection<Wish> WishListItem = new ObservableCollection<Wish>();
        public SubscriptionsPageViewModel SubscriptionsPageViewModelItem { get; private set; }
        public ObservableCollection<Wishlist> WishLists { get; set; }
        private WishListService wishListService;
        public int SelectedId { get; set; }
        public SubscriptionsPage()
        {
            this.InitializeComponent();
            wishListService = WishListService.Instance;
            this.SubscriptionsPageViewModelItem = new SubscriptionsPageViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret = await wishListService.SubscribedWishLists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            ListView1.ItemsSource = WishLists;
        }

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            var selectedWishList = (Wishlist)ListView1.SelectedItem;
            SelectedId = selectedWishList.WishlistId;
            App.Current.NavigationService.Navigate(typeof(SubscriptionsDetailPage), SelectedId);
        }
    }
}
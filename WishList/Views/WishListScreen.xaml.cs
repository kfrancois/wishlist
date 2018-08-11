using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;
using WishList.ViewModels;

namespace WishList.Views
{

    public sealed partial class WishListScreen : Page
    {
        public ObservableCollection<Wishlist> WishLists { get; set; }
        private WishListService wishListService;
        public WishListScreen()
        {
            InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret = await wishListService.GetWishlists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            ListView1.ItemsSource = WishLists;
        }

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            var selectedWishList = (Wishlist)ListView1.SelectedItem;
            //Frame.Navigate(typeof(WishListDetailPage), selectedWishList);
            App.Current.NavigationService.Navigate(typeof(WishListDetailPage), selectedWishList);
        }

        public void NewWishList(object sender, RoutedEventArgs e)
        {
            App.Current.NavigationService.Navigate(typeof(NewWishList));
        }
    }
}

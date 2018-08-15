using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.Views
{

    public sealed partial class WishListScreen : Page
    {
        private WishListService wishListService;

        public ObservableCollection<Wishlist> WishLists { get; set; }
        public int SelectedId { get; set; }



        public WishListScreen()
        {
            InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret1 = await wishListService.GetWishlists();

            WishLists = ret1.Count() == 0 ? new ObservableCollection<Wishlist>() : ret1;

            ListBox.ItemsSource = WishLists;
        }

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            var selectedWishList = (Wishlist)ListBox.SelectedItem;
            if (selectedWishList != null)
            {
                (Window.Current.Content as Frame).Navigate(typeof(WishListDetailPage), selectedWishList.WishlistId);
            }
        }

        public void NewWishList(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(NewWishList));
        }
    }
}
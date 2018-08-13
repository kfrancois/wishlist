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
        public ObservableCollection<Wishlist> WishLists { get; set; }
        private WishListService wishListService;
        public WishListScreen()
        {
            this.InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret1 = await wishListService.GetWishlists();

            WishLists = ret1;

            if (WishLists.Count() > 0)
                ListView1.ItemsSource = WishLists;
        }

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            var selectedWishList = (Wishlist)listBox.SelectedItem;
            (Window.Current.Content as Frame).Navigate(typeof(WishListDetailPage), selectedWishList);
        }

        public void NewWishList(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(NewWishList));
        }
    }
}

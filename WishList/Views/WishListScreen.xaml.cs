using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using WishList.Model;
using WishList.ViewModels;

namespace WishList.Views
{

    public sealed partial class WishListScreen : Page
    {
        //public ObservableCollection<Wishlist> WishListItem = new ObservableCollection<Wishlist>();
        public WishListScreenViewModel WishListScreenViewModelItem { get; private set; }
        public WishListScreen()
        {
            InitializeComponent();
            WishListScreenViewModelItem = new WishListScreenViewModel();

            //ListView1.DataContext = WishListItem;
        }

        /*private void LoadWishlist()
        {
            foreach (Wishlist wish in WishListScreenViewModelItem.WishLists)
            {
                WishListItem.Add(wish);
            }
        }*/

        

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            //var selectedList = (Wishlist) ListView1.SelectedItem;
            //Frame.Navigate(typeof(Views.WishListDetailPage), selectedList);
            Frame.Navigate(typeof(WishListDetailPage));
        }
    }
}

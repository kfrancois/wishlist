using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using WishList.Model;
using WishList.ViewModels;

namespace WishList.Views
{

    public sealed partial class WishListScreen : Page
    {
        public WishListScreenViewModel WishListScreenViewModelItem { get; private set; }
        public WishListScreen()
        {
            InitializeComponent();
            WishListScreenViewModelItem = new WishListScreenViewModel();
        }

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(WishListDetailPage));
        }
    }
}

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
using WishList.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WishList.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WishListScreen : Page
    {
        public ObservableCollection<Wish> WishListItem = new ObservableCollection<Wish>();
        public WishListScreenViewModel WishListScreenViewModelItem { get; private set; }
        public WishListScreen()
        {
            this.InitializeComponent();
            this.WishListScreenViewModelItem = new WishListScreenViewModel();
          
            MakeHardcodeWishlist(); 
            ListView1.DataContext = WishListItem; 
        }

        private void MakeHardcodeWishlist()
        {
            foreach (Wish wish in this.WishListScreenViewModelItem.WishListItem) {
                this.WishListItem.Add(wish); 
            }
        }

        public void ShowDetail(object sender, SelectionChangedEventArgs e)
        {
            //var selectedList = (Wishlist) ListView1.SelectedItem;
            //Frame.Navigate(typeof(Views.WishListDetailPage), selectedList);
            //Frame.Navigate(typeof(Views.WishListDetailPage));
            //Frame parentFrame = Window.Current.Content as Frame;
            //parentFrame.Navigate(typeof(Views.WishListDetailPage));
            App.Current.NavigationService.Frame.Navigate(typeof(Views.WishListDetailPage));
        }
    }
}

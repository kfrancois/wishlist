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
    public sealed partial class WishListOverviewPage : Page
    {
        public ObservableCollection<Wishlist> WishListItem = new ObservableCollection<Wishlist>();
        public WishListOverviewPageViewModel WishListOverviewPageViewModelItem { get; private set; }

        public WishListOverviewPage()
        {
            this.InitializeComponent();
            this.WishListOverviewPageViewModelItem = new WishListOverviewPageViewModel();

            MakeHardcodeWishlist();
            ListView1.DataContext = WishListItem.Take(5);
            ListView2.DataContext = WishListItem.Take(5);
        }

        private void MakeHardcodeWishlist()
        {
            foreach (Wishlist wish in this.WishListOverviewPageViewModelItem.WishList)
            {
                this.WishListItem.Add(wish);
            }
        }
    }
}

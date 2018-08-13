using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class NewWishList : Page
    {

        private WishListService wishListService;

        public NewWishList()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            wishListService = WishListService.Instance;
        }

        public async void SaveAsync(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == null ||NameBox.Text == "")
            {
                NameErr.Text = "Name cannot be empty";
                NameBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Wishlist newList = new Wishlist(NameBox.Text);
                NameErr.Text = "";
                await wishListService.CreateWishlist(newList);
                Frame.GoBack();
            }
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

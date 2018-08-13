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
    public sealed partial class NewWishPage : Page
    {

        public Wishlist WishList { get; set; }
        private WishListService wishListService;

        public NewWishPage()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            wishListService = WishListService.Instance;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            WishList = (Wishlist)e.Parameter;
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == null || NameBox.Text == "")
            {
                NameErr.Text = "Name cannot be empty";
                NameBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (DescriptionBox.Text == null || DescriptionBox.Text == "")
            {
                DescrErr.Text = "Description cannot be empty";
                DescriptionBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (PriceBox.Text == null || PriceBox.Text == "")
            {
                PriceErr.Text = "Price cannot be empty";
                PriceBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (NameBox.Text != null && NameBox.Text != "" && DescriptionBox.Text != null && DescriptionBox.Text != "" && PriceBox.Text != null && PriceBox.Text != "")
            {
                try
                {
                    Wish wish = new Wish(NameBox.Text, DescriptionBox.Text, Double.Parse(PriceBox.Text));
                    NameErr.Text = "";
                    DescrErr.Text = "";
                    PriceErr.Text = "";
                    Save(wish);
                }
                catch (FormatException ex)
                {
                    PriceErr.Text = "Give a valid price";
                }
            }
        }

        private async void Save(Wish wish)
        {
            await wishListService.CreateWish(WishList.WishlistId, wish);
            Frame.GoBack();
        }
    }
}

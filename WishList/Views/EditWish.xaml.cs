using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class EditWish : Page
    {

        public Wish SelectedWish { get; set; }
        private WishService wishService;

        public EditWish()
        {
            this.InitializeComponent();
            wishService = WishService.Instance;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = (Wish)e.Parameter;
            SelectedWish = parameter;
            NameBox.Text = SelectedWish.Name;
            DescriptionBox.Text = SelectedWish.Description;
            PriceBox.Text = SelectedWish.Price.ToString();
        }

        public void GoBack(object sender, RoutedEventArgs e) => Frame.GoBack();

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
                    SelectedWish.Name = NameBox.Text;
                    NameErr.Text = "";
                    SelectedWish.Description = DescriptionBox.Text;
                    DescrErr.Text = "";
                    SelectedWish.Price = Double.Parse(PriceBox.Text);
                    PriceErr.Text = "";
                    Edit(SelectedWish);
                } catch(FormatException ex)
                {
                    PriceErr.Text = "Give a valid price";
                }
            }
        }

        private async void Edit(Wish item)
        {
            await wishService.PutWish(item.WishId, item);
            Frame.GoBack();
        }
    }
}

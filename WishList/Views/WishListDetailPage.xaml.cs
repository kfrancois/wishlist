using System;
using System.Collections.ObjectModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.Views
{

    public sealed partial class WishListDetailPage : Page
    {
        public ObservableCollection<Wish> Wishes { get; set; }
        public Wishlist WishList { get; set; }
        private WishListService wishListService;
        public WishListDetailPage()
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            WishList = await wishListService.GetWishlist(((Wishlist)e.Parameter).WishlistId);
            PageHeader.Text = WishList.Title;
            Wishes = new ObservableCollection<Wish>(WishList.Wishes);
            ListView1.ItemsSource = Wishes;
        }

        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {
            if (ListView1.SelectedIndex == -1) return;

            Wish selected = (Wish)ListView1.SelectedItem;

            if (selected.Claimed == false)
            {
                var dialog = new Windows.UI.Popups.MessageDialog(
                            "Do you want to edit this wish?",
                            "Edit this Wish");

                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Edit") { Id = 0 });
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel") { Id = 1 });

                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                var result = await dialog.ShowAsync();

                if ((int)result.Id == 0)
                {
                    ShowEditPage(selected);
                }
                else
                {
                    ListView1.SelectedItem = null;
                }
            }
            else
            {
                var dialog = new Windows.UI.Popups.MessageDialog(
                            "This wish is already claimed",
                            "Claimed");

                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel") { Id = 1 });

                dialog.CancelCommandIndex = 1;

                var result = await dialog.ShowAsync();
            }

        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public void NewWish(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewWishPage), WishList);
        }


        private void ShowEditPage(Wish item)
        {
            Frame.Navigate(typeof(Views.EditWish), item);
        }
    }
}
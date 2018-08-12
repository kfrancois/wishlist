using System;
using System.Collections.ObjectModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WishList.Model;

namespace WishList.Views
{

    public sealed partial class WishListDetailPage : Page
    {
        public ObservableCollection<Wish> Wishes { get; set; }
        public WishListDetailPage()
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = (Wishlist) e.Parameter;
            pageHeader.Text = parameter.Title;
            Wishes = new ObservableCollection<Wish>(parameter.Wishes);
            ListView1.ItemsSource = Wishes;
        }

        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {

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
                    ShowEditPage();
                }
            } else
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
            Frame.Navigate(typeof(NewWishPage));
        }


        private void ShowEditPage()
        {
            Frame.Navigate(typeof(Views.EditWish));
        }
    }
}

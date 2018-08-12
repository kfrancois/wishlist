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
            Wishes = new ObservableCollection<Wish>(parameter.Wishes);
            ListView1.ItemsSource = Wishes;
        }

        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Do you want to delete or edit this wish?",
                        "Delete/Edit this Wish");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Delete") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Edit") { Id = 1 });

            /*if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                // Adding a 3rd command will crash the app when running on Mobile !!!
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Maybe later") { Id = 2 });
            }*/

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            /*var btn = sender as Button;
            btn.Content = $"Result: {result.Label} ({result.Id})";*/

            if((int) result.Id == 1)
            {
                ShowEditPage();
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

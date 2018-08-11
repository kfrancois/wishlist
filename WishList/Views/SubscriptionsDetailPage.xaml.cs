using System;
using System.Collections.ObjectModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WishList.Model;

namespace WishList.Views
{
    public sealed partial class SubscriptionsDetailPage : Page
    {
        public ObservableCollection<Wish> WishListItem = new ObservableCollection<Wish>();

        public SubscriptionsDetailPage()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
        }


        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Are you sure you want to fulfill this Wish?",
                        "Fulfill this Wish");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

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
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

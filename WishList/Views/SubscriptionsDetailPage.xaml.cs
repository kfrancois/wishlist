using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
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
    public sealed partial class SubscriptionsDetailPage : Page
    {
        public ObservableCollection<Wish> WishListItem = new ObservableCollection<Wish>();
        public SubscriptionsDetailsPageViewModel SubscriptionsDetailPageViewModelItem { get; private set; }

        public SubscriptionsDetailPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            this.SubscriptionsDetailPageViewModelItem = new SubscriptionsDetailsPageViewModel();

            MakeHardcodeWishlist();
            ListView1.DataContext = WishListItem;
        }

        private void MakeHardcodeWishlist()
        {
            foreach (Wish wish in this.SubscriptionsDetailPageViewModelItem.WishListItem)
            {
                this.WishListItem.Add(wish);
            }
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
    }
}

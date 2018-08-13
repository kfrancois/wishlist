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
    public sealed partial class SubscriptionsDetailPage : Page
    {
        public ObservableCollection<Wish> Wishes = new ObservableCollection<Wish>();
        private WishService wishService;

        public SubscriptionsDetailPage()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            wishService = WishService.Instance;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = (Wishlist)e.Parameter;
            PageHeader.Text = parameter.Title;
            Wishes = new ObservableCollection<Wish>(parameter.Wishes);
            ListView1.ItemsSource = Wishes;
        }

        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {
            Wish selected = (Wish)ListView1.SelectedItem;

            if (selected.Claimed == false)
            {
                var dialog = new Windows.UI.Popups.MessageDialog(
                            "Are you sure you want to fulfill this Wish?",
                            "Fulfill this Wish");

                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                var result = await dialog.ShowAsync();
                if ((int)result.Id == 0)
                {
                    Claime(selected);
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

        private async void Claime(Wish selected)
        {
            selected.Claimed = true;
            await wishService.PutWish(selected.WishId, selected);
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

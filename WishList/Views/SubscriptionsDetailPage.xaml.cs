using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
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
        public ObservableCollection<Wish> OpenWishes = new ObservableCollection<Wish>();
        public ObservableCollection<Wish> ClaimedWishes = new ObservableCollection<Wish>();
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
            var wishlist = (Wishlist)e.Parameter;
            PageHeader.Text = $"{wishlist.Title} by {wishlist.CreatorName}";
            OpenWishes = new ObservableCollection<Wish>(wishlist.Wishes.FindAll(w => !w.Claimed));
            ClaimedWishes = new ObservableCollection<Wish>(wishlist.Wishes.FindAll(w => w.Claimed));
            OpenWishesListBox.ItemsSource = OpenWishes;
            ClaimedWishesListBox.ItemsSource = ClaimedWishes;
        }

        private async void OpenWishSelected(object sender, RoutedEventArgs e)
        {
            Wish wish = (Wish)OpenWishesListBox.SelectedItem;

            if (wish == null) return;

            var text = "Are you sure you want to fulfill this Wish?";
            var title = "Fulfill this Wish";

            if (await ShowDialog(text, title))
            {
                ClaimWish(wish);
            }
            else
            {
                OpenWishesListBox.SelectedItem = null;
            }
        }

        private async Task<bool> ShowDialog(string text, string title)
        {
            var dialog = new MessageDialog(text, title);

            dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();
            return result.Id.ToString() == "0";
        }

        private async void ClaimWish(Wish wish)
        {
            wish.Claimed = true;
            await wishService.PutWish(wish.WishId, wish);

            OpenWishes.Remove(wish);
            ClaimedWishes.Add(wish);
        }

        public void GoBack(object sender, RoutedEventArgs e) => Frame.GoBack();
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
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
    public sealed partial class HomeFrame : Page
    {

        private ApiService apiService;
        private WishListService wishListService;

        public HomeFrame()
        {
            this.InitializeComponent();
            apiService = ApiService.Instance;
            wishListService = WishListService.Instance;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            User.Text = apiService.GetUser();
            ObservableCollection<Wishlist> WishLists = await wishListService.InvitedWishLists();
            SendToast("You have got " + WishLists.Count() + " invite(s).");
        }

        private void SendToast(string context)
        {
            //build toast
            var template = ToastTemplateType.ToastText01;
            var xml = ToastNotificationManager.GetTemplateContent(template);

            //set value
            var text = xml.CreateTextNode(context);
            var elements = xml.GetElementsByTagName("text");
            elements[0].AppendChild(text);

            //show toast
            var toast = new ToastNotification(xml);
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(toast);
        }
    }
}

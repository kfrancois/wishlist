﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class InvitesPage : Page
    {
        public ObservableCollection<Wishlist> WishLists { get; set; }
        private WishListService wishListService;

        public InvitesPage()
        {
            this.InitializeComponent();
            wishListService = WishListService.Instance;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ObservableCollection<Wishlist> ret = await wishListService.InvitedWishLists();

            WishLists = ret.Count() == 0 ? new ObservableCollection<Wishlist>() : ret;

            ListView1.ItemsSource = WishLists;
        }

        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Are you sure you want to accept this wishlist?",
                        "Accept this invite");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            /*if (result.Id.ToString() == "0")
            {
                AcceptInviteAsync(item);
            }*/

            System.Diagnostics.Debug.WriteLine(result.Id);
        }

        private async void AcceptInviteAsync(Wishlist item)
        {
            await wishListService.AcceptInvite(item.WishlistId);
        }

        public void NewInvite(object sender, RoutedEventArgs e)
        {
            App.Current.NavigationService.Navigate(typeof(Views.NewInvite));
        }
    }
}

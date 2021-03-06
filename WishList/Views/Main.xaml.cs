﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WishList.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {

        private List<Tuple<string, Type>> _pages = new List<Tuple<string, Type>>
        {
            new Tuple<string, Type>("home", typeof(HomeFrame)),
            new Tuple<string, Type>("own", typeof(WishListScreen)),
            new Tuple<string, Type>("subs", typeof(SubscriptionsPage)),
            new Tuple<string, Type>("invites", typeof(InvitesPage)),
            new Tuple<string, Type>("browse", typeof(Browse))
        };

        public Main()
        {
            InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsFrame));
            }
            else
            {
                // Getting the Tag from Content (args.InvokedItem is the content of NavigationViewItem)
                var navItemTag = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(i => args.InvokedItem.Equals(i.Content))
                    .Tag.ToString();
                NavView_Navigate(navItemTag);
            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;
            // NavView doesn't load any page by default: you need to specify it
            NavView_Navigate("home");

        }

        private void NavView_Navigate(string navItemTag)
        {
            var item = _pages.First(p => p.Item1.Equals(navItemTag));
            ContentFrame.Navigate(item.Item2);
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            if (ContentFrame.SourcePageType == typeof(SettingsFrame))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Item2 == e.SourcePageType);
                if (item != null)
                {
                    NavView.SelectedItem = NavView.MenuItems
                        .OfType<NavigationViewItem>()
                        .First(n => n.Tag.Equals(item.Item1));
                }
            }
        }
    }
}

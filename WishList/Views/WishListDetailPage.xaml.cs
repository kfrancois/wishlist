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
    public sealed partial class WishListDetailPage : Page
    {
        public ObservableCollection<Wish> WishListItem = new ObservableCollection<Wish>();
        public WishListDetailPageViewModel WishListDetailPageViewModelItem { get; private set; }
        public WishListDetailPage()
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            this.InitializeComponent();
            this.WishListDetailPageViewModelItem = new WishListDetailPageViewModel();

            MakeHardcodeWishlist();
            ListView1.DataContext = WishListItem;
        }

        private void MakeHardcodeWishlist()
        {
            foreach (Wish wish in this.WishListDetailPageViewModelItem.WishListItem)
            {
                this.WishListItem.Add(wish);
            }
        }
    }
}

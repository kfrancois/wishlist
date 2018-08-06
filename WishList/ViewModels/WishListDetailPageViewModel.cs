using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using WishList.Model;

namespace WishList.ViewModels
{
    public class WishListDetailPageViewModel : ViewModelBase
    {
        public string Name { get; private set; }
        public int SelectedList { get; set; }

        public WishListDetailPageViewModel()
        {}

        public void GotoNewWish() => NavigationService.Navigate(typeof(Views.NewWishPage));

        public void GoBack() => NavigationService.GoBack();
    }
}

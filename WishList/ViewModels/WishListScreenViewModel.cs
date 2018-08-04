using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using WishList.Model;
using WishList.Services;

namespace WishList.ViewModels
{
    public class WishListScreenViewModel : ViewModelBase
    {
        public string Name { get; private set; }

        public ObservableCollection<Wishlist> WishLists { get; set; }

        public Wishlist SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                if (value == _selectedItem)
                    return;

                _selectedItem = value;

                System.Diagnostics.Debug.WriteLine(_selectedItem.Title);

                NavigationService.Navigate(typeof(Views.WishListDetailPage), _selectedItem);
            }
        }
        private ApiService apiService;
        private Wishlist _selectedItem;

        public WishListScreenViewModel()
        {
            apiService = ApiService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            var result = await apiService.GetContentFromResponse(await apiService.SendRequest(RequestType.GET, "wishlist/all"));

            WishLists =  JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(result);
            
            System.Diagnostics.Debug.WriteLine("Collection: " + WishLists.First().Title);
        }

        public void GoBack() =>
            NavigationService.Navigate(typeof(Views.MainPage));

        public void GotoNewWishList() =>
            NavigationService.Navigate(typeof(Views.NewWishList));
    }
}

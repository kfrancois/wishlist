using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using WishList.Model;
using WishList.Services;

namespace WishList.ViewModels
{
    public class WishListScreenViewModel : ViewModelBase
    {
        public string Name { get; private set; }

        public ObservableCollection<Wishlist> WishLists { get; set; }

        //public ObservableCollection<Wishlist> WishListCollection { get; set; }

        private ApiService apiService;

        public WishListScreenViewModel()
        {
            apiService = ApiService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            var result = await apiService.GetContentFromResponse(await apiService.SendRequest(RequestType.GET, "wishlist/all"));

            WishLists =  JsonConvert.DeserializeObject<ObservableCollection<Wishlist>>(result);

            //WishListCollection = new ObservableCollection<Wishlist>(WishLists);

            //System.Diagnostics.Debug.WriteLine("List: " + WishLists.First().Title);
            System.Diagnostics.Debug.WriteLine("Collection: " + WishLists.First().Title);
        }

        public void GotoNewWishList() =>
            App.Current.NavigationService.Navigate(typeof(Views.NewWishList));
    }
}

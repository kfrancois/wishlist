using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public List<Wishlist> WishLists { get; set; }

        private ApiService apiService;

        public WishListScreenViewModel()
        {
            apiService = ApiService.Instance;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            var result = await apiService.GetContentFromResponse(await apiService.SendRequest(RequestType.GET, "wishlist/all"));

            WishLists =  JsonConvert.DeserializeObject<List<Wishlist>>(result);

            Console.WriteLine(WishLists);
        }

        public void GotoNewWishList() =>
            App.Current.NavigationService.Navigate(typeof(Views.NewWishList));
    }
}

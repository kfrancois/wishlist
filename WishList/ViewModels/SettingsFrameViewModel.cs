using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace WishList.ViewModels
{
    public class SettingsFrameViewModel : ViewModelBase
    {
        public void GotoLogin()
        {
            //WishListService.AuthenticateUser("firstUser@hogent.be", "P@ssword1");
            //NavigationService.Navigate(typeof(Views.LoginPage));

            App.Current.NavigationService.Navigate(typeof(Views.LoginPage));
        }
    }
}

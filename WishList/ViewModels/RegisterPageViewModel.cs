using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace WishList.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public void GotoLoginPage() =>
            NavigationService.Navigate(typeof(Views.LoginPage));

        public void GotoMainPage() =>
             NavigationService.Navigate(typeof(Views.MainPage));
    }
}

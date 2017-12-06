using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace WishList.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        /*public LoginPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Testing";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }*/

        public void GotoMainPage() =>
            NavigationService.Navigate(typeof(Views.MainPage));

        public void GotoRegisterPage() =>
            NavigationService.Navigate(typeof(Views.RegisterPage));
    }
}

using Newtonsoft.Json;
using System;
using System.Text;
using Template10.Mvvm;
using Windows.Web.Http;
using WishList.Services;

namespace WishList.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private ApiService apiService;

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public LoginPageViewModel()
        {
            apiService = ApiService.Instance;
        }

        public void GotoMainPage()
        {
            Login();
        }

        public void Login()
        {

            if(UserName == null)
            {
                ErrorMessage = "Username cannot be empty!";
            }
            else if (Password == null)
            {
                ErrorMessage = "Password cannot be empty!";
            }
            else
            {
                apiService.SaveLoginDetails(UserName, Password); // TODO
                NavigationService.Navigate(typeof(Views.Main));
            }

        }

        public void GotoRegisterPage() =>
            NavigationService.Navigate(typeof(Views.RegisterPage));
    }
}

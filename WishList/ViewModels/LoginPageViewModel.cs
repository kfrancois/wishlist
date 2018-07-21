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

        public LoginPageViewModel()
        {
            apiService = ApiService.Instance;
        }

        public void GotoMainPage()
        {
            Login();
        }

        private async void Login()
        {
            apiService.SaveLoginDetails("firstUser@hogent.be", "P@ssword1"); // TODO

            NavigationService.Navigate(typeof(Views.MainPage));

        }

        public void GotoRegisterPage() =>
            NavigationService.Navigate(typeof(Views.RegisterPage));
    }
}

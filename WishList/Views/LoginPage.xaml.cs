using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WishList.Services;

namespace WishList.Views
{

    public sealed partial class LoginPage : Page
    {

        private ApiService apiService;

        public LoginPage()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            apiService = ApiService.Instance;
        }

        public async void Login()
        {
            var Email = EmailField.Text;
            var Password = PasswordField.Password;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                // TODO: Enable 
                // ErrorMessage.Text = "Please fill in both e-mail and password";
                await apiService.SaveLoginDetails("firstUser@hogent.be", "P@ssword1"); // TODO: Disable this
                Frame.Navigate(typeof(Main));
            }
            else
            {
                await apiService.SaveLoginDetails(Email, Password);
                Frame.Navigate(typeof(Main));
            }
        }

        private void OnKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Login();
            }
        }

        public void LoginClicked(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void RegisterClicked(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
        }
    }
}
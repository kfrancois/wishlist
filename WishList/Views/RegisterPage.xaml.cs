using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WishList.Services;

namespace WishList.Views
{
    public sealed partial class RegisterPage : Page
    {
        private ApiService apiService;

        public RegisterPage()
        {
            InitializeComponent();
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            apiService = ApiService.Instance;
        }

        public void GotoLoginPage(object sender, RoutedEventArgs e) => Frame.GoBack();

        public async void Register()
        {

            var Email = EmailField.Text;
            var Password = PasswordField.Password;
            var ConfirmPassword = ConfirmPasswordField.Password;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage.Text = "Please fill in both e-mail and password";
            }
            else if (Password.Length < 6)
            {
                PasswordErrorMessage.Text = "Password should be at least 6 characters";
            }
            else if (Password != ConfirmPassword)
            {
                PasswordErrorMessage.Text = "Passwords don't match";
            }
            else
            {
                await apiService.Register(Email, Password);
                Frame.Navigate(typeof(Main));
            }
        }

        public void RegisterClicked(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void OnKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            PasswordErrorMessage.Text = "";
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Register();
            }
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
        }
    }
}

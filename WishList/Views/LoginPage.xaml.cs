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

        public void Login(object sender, RoutedEventArgs e)
        {

            /*if (UserName == null)
            {
                ErrorMessage = "Username cannot be empty!";
            }
            else if (Password == null)
            {
                ErrorMessage = "Password cannot be empty!";
            }
            else
            {*/
            apiService.SaveLoginDetails("firstUser@hogent.be", "P@ssword1"); // TODO
            Frame.Navigate(typeof(Main));
            //}

        }

        private void RegisterButtonTextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
        }
    }
}

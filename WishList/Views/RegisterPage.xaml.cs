using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        public async void Register(object sender, RoutedEventArgs e)
        {
            await apiService.Register("thirdUser@hogent.be", "P@ssword1");
            Frame.Navigate(typeof(Main));
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
        }
    }
}

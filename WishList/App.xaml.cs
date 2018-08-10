using System.Threading.Tasks;
using WishList.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Common;
using Windows.UI.Xaml.Data;
using WishList.Services;

namespace WishList
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {

        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion
        }


        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            var apiService = ApiService.Instance;

            await NavigationService.NavigateAsync(apiService.IsSaved("login") ? typeof(Views.Main) : typeof(Views.LoginPage));
        }
    }
}

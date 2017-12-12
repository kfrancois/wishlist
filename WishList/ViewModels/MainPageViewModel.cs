using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace WishList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    { 

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        /*public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);*/

        public void GotoLogin() =>
            NavigationService.Navigate(typeof(Views.LoginPage));

        public class SettingsPartViewModel : ViewModelBase
        {
            Services.SettingsServices.SettingsService _settings;

            public SettingsPartViewModel()
            {
                if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                {
                    // designtime
                }
                else
                {
                    _settings = Services.SettingsServices.SettingsService.Instance;
                }
            }

            public bool UseShellBackButton {
                get { return _settings.UseShellBackButton; }
                set { _settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
            }

            public bool UseLightThemeButton {
                get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
                set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
            }

            private string _BusyText = "Please wait...";
            public string BusyText {
                get { return _BusyText; }
                set {
                    Set(ref _BusyText, value);
                    _ShowBusyCommand.RaiseCanExecuteChanged();
                }
            }

            DelegateCommand _ShowBusyCommand;
            public DelegateCommand ShowBusyCommand
                => _ShowBusyCommand ?? (_ShowBusyCommand = new DelegateCommand(async () =>
                {
                    Views.Busy.SetBusy(true, _BusyText);
                    await Task.Delay(5000);
                    Views.Busy.SetBusy(false);
                }, () => !string.IsNullOrEmpty(BusyText)));
        }

        public class AboutPartViewModel : ViewModelBase
        {
            public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

            public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

            public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

            public string Version {
                get {
                    var v = Windows.ApplicationModel.Package.Current.Id.Version;
                    return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
                }
            }

            public Uri RateMe => new Uri("http://aka.ms/template10");
        }
    }
}

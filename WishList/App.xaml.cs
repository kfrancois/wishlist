using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WishList.Services;

namespace WishList
{
    public partial class App
    {

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {

            var apiService = ApiService.Instance;
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(apiService.IsSaved("login") ? typeof(Views.Main) : typeof(Views.LoginPage));
            }

            Window.Current.Activate();
        }
    }
}

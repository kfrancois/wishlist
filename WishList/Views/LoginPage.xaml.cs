﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WishList.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WishList.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            App.Current.NavigationService.Navigate(typeof(Views.Main));
            //}

        }

        private void RegisterButtonTextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            App.Current.NavigationService.Navigate(typeof(Views.RegisterPage));
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = 1;
        }
    }
}
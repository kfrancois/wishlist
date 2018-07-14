using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WishList.ViewModels
{
    public class InvitesPageViewModel : ViewModelBase
    {
        public void GotoNewInvites()
        {
            App.Current.NavigationService.Navigate(typeof(Views.NewInvite));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace WishList.ViewModels
{
    public class InvitesPageViewModel : ViewModelBase
    {
        public void GotoNewInvites() => NavigationService.Navigate(typeof(Views.NewInvite));

        public void GoBack() => NavigationService.Navigate(typeof(Views.Main));
    }
}

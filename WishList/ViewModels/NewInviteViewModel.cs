﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace WishList.ViewModels
{
    public class NewInviteViewModel : ViewModelBase
    {
        public void GoBack() => NavigationService.GoBack();
    }
}

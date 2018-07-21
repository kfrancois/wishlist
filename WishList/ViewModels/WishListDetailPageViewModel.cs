﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using WishList.Model;

namespace WishList.ViewModels
{
    public class WishListDetailPageViewModel : ViewModelBase
    {
        public string Name { get; private set; }
        public List<Wish> WishListItem { get; set; } = new List<Wish>();

        public WishListDetailPageViewModel()
        {
            MakeHardcodeWishlist();
        }

        private void MakeHardcodeWishlist()
        {
            for (int i = 0; i < 15; i++)
            {
                WishListItem.Add(new Wish(new Category("Birthday"), "Title" + (i + 1).ToString(), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Mauris at eleifend augue.Cras mattis, nisi id aliquam ornare, magna leo elementum arcu, ut porttitor mi metus eget ligula.", (i + 1) * 2.46));
            }
        }

        public void GoBack() => NavigationService.GoBack();

        public void GotoNewWish() => NavigationService.Navigate(typeof(Views.NewWishPage));
    }
}

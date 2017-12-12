using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WishListRestService.Models;

namespace WishListRestService.Services {
    public class WishListRepository : IWishListRepository {
        private List<WishList> _wishLists;

        public WishListRepository() {
            InitializeData();
        }

        private void InitializeData() {
            _wishLists = new List<WishList>();

            _wishLists.Add(new WishList {
                Title = "New list"
            });
        }

        public bool DoesItemExist(int id) {
            return _wishLists.Any(wl => wl.WishListId == id);
        }

        public IEnumerable<WishList> All => _wishLists;

        public WishList Find(int id) {
            return _wishLists.FirstOrDefault(wl => wl.WishListId == id);
        }

        public void Insert(WishList item) {
            _wishLists.Add(item);
        }

        public void Update(WishList item) {
            var wishList = Find(item.WishListId);
            var index = _wishLists.IndexOf(wishList);
            _wishLists.RemoveAt(index);
            _wishLists.Insert(index, item);
        }

        public void Delete(int id) {
            _wishLists.Remove(Find(id));
        }
    }
}
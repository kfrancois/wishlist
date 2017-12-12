using System.Collections.Generic;
using WishListRestService.Models;

namespace WishListRestService.Services {
    public interface IWishListRepository {
        bool DoesItemExist(int id);
        IEnumerable<WishList> All { get; }
        WishList Find(int id);
        void Insert(WishList item);
        void Update(WishList item);
        void Delete(int id);
    }
}
using System.Collections.Generic;
using WishListRestService.Models;

namespace WishListRestService.Repository
{
    public interface IWishListRepository
    {
        void Add(WishList item);
        IEnumerable<WishList> GetAll();
        WishList Find(int id);
        void Remove(int id);
        void Update(WishList item);
        void SaveChanges();
    }
}
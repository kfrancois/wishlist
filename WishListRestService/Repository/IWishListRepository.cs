using System.Collections.Generic;
using WishListRestService.Models;

namespace WishListRestService.Repository
{
    public interface IWishListRepository
    {
        void Add(Wishlist item);
        IEnumerable<Wishlist> GetAll();
        Wishlist Find(int id);
        void Remove(int id);
        void Update(Wishlist item);
        void SaveChanges();
    }
}
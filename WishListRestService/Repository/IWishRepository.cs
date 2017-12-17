using System.Collections.Generic;
using WishListRestService.Models;

namespace WishListRestService.Repository
{
    public interface IWishRepository
    {
        void Add(Wish item);
        IEnumerable<Wish> GetAll();
        Wish Find(int id);
        void Remove(int id);
        void Update(Wish item);
        void SaveChanges();
    }
}

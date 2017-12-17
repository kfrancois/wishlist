using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
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

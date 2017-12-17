using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
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
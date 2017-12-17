using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class WishRepository : IWishRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Wish> _wishes;

        public WishRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _wishes = dbContext.Wishes;
        }

        public void Add(Wish item)
        {
            _wishes.Add(item);
            SaveChanges();
        }

        public IEnumerable<Wish> GetAll()
        {
            return _wishes;
        }

        public Wish Find(int id)
        {
            return _wishes.SingleOrDefault(w => w.WishId == id);
        }

        public void Remove(int id)
        {
            var itemToRemove = _wishes.SingleOrDefault(w => w.WishId == id);
            if (itemToRemove != null)
                _wishes.Remove(itemToRemove);
            SaveChanges();
        }

        public void Update(Wish item)
        {
            var itemToUpdate = _wishes.SingleOrDefault(w => w.WishId == item.WishId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Claimed = item.Claimed; // TODO update other fields
            }
            SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
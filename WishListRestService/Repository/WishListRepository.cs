using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WishListRestService.Data;
using WishListRestService.Models;

namespace WishListRestService.Repository
{
    [Authorize]
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Wishlist> _wishLists;

        public WishListRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _wishLists = dbContext.WishLists;
        }

        public void Add(Wishlist item)
        {
            _wishLists.Add(item);
            SaveChanges();
        }

        public Wishlist Find(int id)
        {
            return _wishLists.Include(wl => wl.Wishes).Include(wl => wl.PendingInvites).Include(wl => wl.Subscribers)
                .SingleOrDefault(wl => wl.WishlistId == id);
        }

        public IEnumerable<Wishlist> GetAll()
        {
            return _wishLists.Include(wl => wl.Wishes).Include(wl => wl.PendingInvites).Include(wl => wl.Subscribers);
        }

        public void Remove(int id)
        {
            var itemToRemove = _wishLists.SingleOrDefault(wl => wl.WishlistId == id);
            if (itemToRemove != null)
                _wishLists.Remove(itemToRemove);
            SaveChanges();
        }

        public void Update(Wishlist item)
        {
            var itemToUpdate = _wishLists.SingleOrDefault(wl => wl.WishlistId == item.WishlistId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Title = item.Title; // TODO update other fields
                itemToUpdate.CreatorName = item.CreatorName;
                itemToUpdate.PendingInvites = item.PendingInvites;
                itemToUpdate.Subscribers = item.Subscribers;
                itemToUpdate.Wishes = item.Wishes;
            }
            SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
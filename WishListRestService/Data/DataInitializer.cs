using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WishListRestService.Models;

namespace WishListRestService.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await InitializeUsers();

                _dbContext.SaveChanges();
            }
        }

        private async Task InitializeUsers()
        {
            var firstUser = new ApplicationUser {UserName = "firstUser@hogent.be", Email = "firstUser@hogent.be"};

            var wishList = new WishList("Birthday")
            {
                CreatorName = firstUser.UserName,
                Wishes = new List<Wish>
                {
                    new Wish {Name = "Car", Description = "Vehicle with four wheels", Price = 10000},
                    new Wish {Name = "Teddy bear", Description = "Plushy toy", Price = 20, Claimed = true},
                    new Wish {Name = "iPhone X", Description = "Cool phone", Price = 999999},
                    new Wish {Name = "Guitar", Description = "For playing music", Price = 200, Claimed = true},
                    new Wish {Name = "Money", Description = "It's money", Price = 20, Claimed = true}
                }
            };

            firstUser.CreateWishList(wishList);

            firstUser.CreateWishList(new WishList("Christmas"));

            var secondUser = new ApplicationUser {UserName = "secondUser@hogent.be", Email = "secondUser@hogent.be"};

            var secondWishList = new WishList("Easter")
            {
                CreatorName = secondUser.UserName,
                Wishes = new List<Wish>
                {
                    new Wish {Name = "New Wish"}
                }
            };

            secondUser.CreateWishList(secondWishList);

            var invite = new PendingInvite
            {
                User = firstUser,
                WishList = secondWishList
            };

            var subscription = new WishListSubscriber
            {
                User = secondUser,
                WishList = wishList
            };


            secondWishList.AddInvite(invite);
            secondUser.Subscribe(subscription);

            await _userManager.CreateAsync(firstUser, "P@ssword1");
            await _userManager.CreateAsync(secondUser, "P@ssword1");
        }
    }
}
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
            string eMailAddress = "firstUser@hogent.be";
            ApplicationUser user = new ApplicationUser {UserName = eMailAddress, Email = eMailAddress};
            await _userManager.CreateAsync(user, "P@ssword1");

            eMailAddress = "secondUser@hogent.be";
            user = new ApplicationUser {UserName = eMailAddress, Email = eMailAddress};
            await _userManager.CreateAsync(user, "P@ssword1");
        }
    }
}
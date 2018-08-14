using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WishListRestService.Data.Mapping;
using WishListRestService.Models;

namespace WishListRestService.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Wishlist> WishLists { get; set; }
        public DbSet<Wish> Wishes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new PendingInviteConfiguration());
            builder.ApplyConfiguration(new PendingRequestConfiguration());
            builder.ApplyConfiguration(new WishConfiguration());
            builder.ApplyConfiguration(new WishListConfiguration());
            builder.ApplyConfiguration(new WishListSubscriberConfiguration());
        }
    }
}
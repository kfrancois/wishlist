using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WishListRestService.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        public List<WishList> PendingInvites { get; set; }

        public List<WishList> CreatedWishLists { get; set; }

        public List<WishList> SubbedWishLists { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,
            string authenticationType) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext()
            : base("DefaultConnection", false) {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            ConfigureUser(modelBuilder.Entity<ApplicationUser>());
            ConfigureWishList(modelBuilder.Entity<WishList>());
            ConfigureWish(modelBuilder.Entity<Wish>());
        }

        private void ConfigureWish(EntityTypeConfiguration<Wish> wish) {
            wish.ToTable("Wish");
            wish.HasKey(w => w.WishId);
        }

        private void ConfigureWishList(EntityTypeConfiguration<WishList> wishList) {
            wishList.ToTable("WishList");
            wishList.HasKey(w => w.WishListId);
            wishList.HasMany(wl => wl.Wishes);
        }

        private void ConfigureUser(EntityTypeConfiguration<ApplicationUser> user) {
            user.HasMany(u => u.CreatedWishLists).WithRequired(wl => wl.Creator);
            user.HasMany(u => u.SubbedWishLists).WithMany().Map(x => {
                x.ToTable("Subscribers");
                x.MapLeftKey("UserId");
                x.MapRightKey("WishListId");
            });
            user.HasMany(u => u.PendingInvites).WithMany(wl => wl.PendingInvites).Map(x => {
                x.ToTable("PendingInvites");
                x.MapLeftKey("UserId");
                x.MapRightKey("WishListId");
            });
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}
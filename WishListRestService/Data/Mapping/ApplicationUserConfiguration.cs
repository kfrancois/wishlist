using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data.Mapping
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasMany(u => u.CreatedWishLists).WithOne();
        }
    }
}
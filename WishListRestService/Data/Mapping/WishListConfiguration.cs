using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data.Mapping
{
    public class WishListConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("WishList");
            builder.HasKey(wl => wl.WishlistId);
            builder.Property(wl => wl.Title);

            builder.HasMany(wl => wl.Wishes).WithOne();
        }
    }
}
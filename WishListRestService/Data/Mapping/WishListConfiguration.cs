using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data.Mapping
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("WishList");
            builder.HasKey(wl => wl.WishListId);
            builder.Property(wl => wl.Title);

            builder.HasMany(wl => wl.Wishes).WithOne();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data.Mapping
{
    public class WishConfiguration : IEntityTypeConfiguration<Wish>
    {
        public void Configure(EntityTypeBuilder<Wish> builder)
        {
            builder.ToTable("Wish");
            builder.HasKey(w => w.WishId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data.Mapping
{
    public class WishListSubscriberConfiguration : IEntityTypeConfiguration<WishlistSubscriber>
    {
        public void Configure(EntityTypeBuilder<WishlistSubscriber> builder)
        {
            builder.HasKey(w => new
            {
                w.WishListId,
                w.UserId
            });

            builder.HasOne(w => w.WishList)
                .WithMany(w => w.Subscribers)
                .HasForeignKey(w => w.WishListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.User)
                .WithMany(w => w.SubbedWishLists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
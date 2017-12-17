using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Data.Mapping
{
    public class WishListSubscriberConfiguration : IEntityTypeConfiguration<WishListSubscriber>
    {
        public void Configure(EntityTypeBuilder<WishListSubscriber> builder)
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
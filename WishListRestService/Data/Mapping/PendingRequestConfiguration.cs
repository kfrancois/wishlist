using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data
{
    public class PendingRequestConfiguration : IEntityTypeConfiguration<PendingRequest>
    {
        public void Configure(EntityTypeBuilder<PendingRequest> builder)
        {
            builder.HasKey(w => new
            {
                w.WishListId,
                w.UserId
            });

            builder.HasOne(w => w.WishList)
                .WithMany(w => w.PendingRequests)
                .HasForeignKey(w => w.WishListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.User)
                .WithMany(w => w.PendingRequests)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
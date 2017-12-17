using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishListRestService.Models;

namespace WishListRestService.Data.Mapping
{
    public class PendingInviteConfiguration : IEntityTypeConfiguration<PendingInvite>
    {
        public void Configure(EntityTypeBuilder<PendingInvite> builder)
        {
            builder.HasKey(w => new
            {
                w.WishListId,
                w.UserId
            });

            builder.HasOne(w => w.WishList)
                .WithMany(w => w.PendingInvites)
                .HasForeignKey(w => w.WishListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.User)
                .WithMany(w => w.PendingInvites)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
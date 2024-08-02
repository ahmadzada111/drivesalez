using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class ImageUrlConfiguration : IEntityTypeConfiguration<ImageUrl>
{
    public void Configure(EntityTypeBuilder<ImageUrl> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Announcement)
            .WithMany(e => e.ImageUrls)
            .HasForeignKey(e => e.AnnouncementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ProfilePhotoBusinessAccount)
            .WithOne(e => e.ProfilePhotoUrl)
            .HasForeignKey<ImageUrl>(e => e.ProfilePhotoUserId)
            .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(e => e.BackgroundPhotoBusinessAccount)
            .WithOne(e => e.BackgroundPhotoUrl)
            .HasForeignKey<ImageUrl>(e => e.BackgroundPhotoUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
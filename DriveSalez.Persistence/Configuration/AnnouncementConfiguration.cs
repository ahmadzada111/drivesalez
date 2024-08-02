using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Vehicle)
            .WithMany(e => e.Announcements)
            .HasForeignKey(e => e.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(e => e.Barter)
            .IsRequired(false);

        builder.Property(e => e.OnCredit)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false);

        builder.HasMany(e => e.ImageUrls)
            .WithOne(e => e.Announcement)
            .HasForeignKey(e => e.AnnouncementId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(e => e.Price)
            .IsRequired();

        builder.Property(e => e.AnnouncementState)
            .IsRequired();

        builder.HasOne(e => e.Country)
            .WithMany(e => e.Announcements)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired();

        builder.HasOne(e => e.City)
            .WithMany(e => e.Announcements)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired();

        builder.Property(e => e.ExpirationDate)
            .IsRequired();

        builder.Property(e => e.IsPremium)
            .IsRequired();

        builder.Property(e => e.ViewCount)
            .HasDefaultValue(0)
            .IsRequired();

        builder.HasOne(e => e.Owner)
            .WithMany(u => u.Announcements)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired();
    }
}
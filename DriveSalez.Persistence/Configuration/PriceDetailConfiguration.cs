using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class PriceDetailConfiguration : IEntityTypeConfiguration<PriceDetail>
{
    public void Configure(EntityTypeBuilder<PriceDetail> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Price)
            .IsRequired();

        builder.HasMany(e => e.Subscriptions)
            .WithOne(e => e.Price)
            .HasForeignKey(e => e.PriceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.AnnouncementTypePricings)
            .WithOne(e => e.Price)
            .HasForeignKey(e => e.PriceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
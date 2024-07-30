using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class AnnouncementTypePricingConfiguration : IEntityTypeConfiguration<AnnouncementTypePricing>
{
    public void Configure(EntityTypeBuilder<AnnouncementTypePricing> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(e => e.Price)
            .WithMany(e => e.AnnouncementTypePricings)
            .HasForeignKey(e => e.PriceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
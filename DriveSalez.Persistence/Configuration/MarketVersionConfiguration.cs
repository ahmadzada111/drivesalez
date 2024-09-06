using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class MarketVersionConfiguration : IEntityTypeConfiguration<MarketVersion>
{
    public void Configure(EntityTypeBuilder<MarketVersion> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Version)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.MarketVersion)
            .HasForeignKey(e => e.MarketVersionId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
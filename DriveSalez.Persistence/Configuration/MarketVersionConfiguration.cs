using DriveSalez.Domain.Entities.VehicleParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class MarketVersionConfiguration : IEntityTypeConfiguration<MarketVersion>
{
    public void Configure(EntityTypeBuilder<MarketVersion> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Version)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.MarketVersion)
            .HasForeignKey(e => e.MarketVersionId)
            .IsRequired();
    }
}
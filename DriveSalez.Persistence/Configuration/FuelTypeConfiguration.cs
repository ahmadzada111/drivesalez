using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.FuelType)
            .HasForeignKey(e => e.FuelTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
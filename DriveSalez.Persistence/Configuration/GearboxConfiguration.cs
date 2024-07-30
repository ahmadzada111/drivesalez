using DriveSalez.Domain.Entities.VehicleParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class GearboxTypeConfiguration : IEntityTypeConfiguration<GearboxType>
{
    public void Configure(EntityTypeBuilder<GearboxType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.GearboxType)
            .HasForeignKey(e => e.GearboxTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
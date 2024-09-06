using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class GearboxTypeConfiguration : IEntityTypeConfiguration<GearboxType>
{
    public void Configure(EntityTypeBuilder<GearboxType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.GearboxType)
            .HasForeignKey(e => e.GearboxTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
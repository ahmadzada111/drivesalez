using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class DrivetrainTypeConfiguration : IEntityTypeConfiguration<DrivetrainType>
{
    public void Configure(EntityTypeBuilder<DrivetrainType> builder)
    {
        builder.HasKey(e => e.Id);

         builder.Property(e => e.Type)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.DrivetrainType)
            .HasForeignKey(e => e.DrivetrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
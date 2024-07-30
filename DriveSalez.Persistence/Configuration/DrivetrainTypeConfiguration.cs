using DriveSalez.Domain.Entities.VehicleParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class DrivetrainTypeConfiguration : IEntityTypeConfiguration<DrivetrainType>
{
    public void Configure(EntityTypeBuilder<DrivetrainType> builder)
    {
        builder.HasKey(e => e.Id);

         builder.Property(e => e.Type)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.DrivetrainType)
            .HasForeignKey(e => e.DrivetrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
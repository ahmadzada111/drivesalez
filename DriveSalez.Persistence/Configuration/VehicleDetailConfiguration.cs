using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class VehicleDetailConfiguration : IEntityTypeConfiguration<VehicleDetail>
{
    public void Configure(EntityTypeBuilder<VehicleDetail> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.FuelType)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.FuelTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Year)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.YearId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.BodyType)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.BodyTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Color)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.ColorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.IsBrandNew)
            .IsRequired(false);

        builder.Property(e => e.HorsePower)
            .IsRequired();

        builder.HasOne(e => e.GearboxType)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.GearboxTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.DrivetrainType)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.DrivetrainTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Conditions)
            .WithMany(e => e.VehicleDetails);

        builder.HasOne(e => e.MarketVersion)
            .WithMany(e => e.VehicleDetails)
            .HasForeignKey(e => e.MarketVersionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.OwnerQuantity)
            .IsRequired(false);

        builder.Property(e => e.SeatCount)
            .IsRequired(false);

        builder.Property(e => e.VinCode)
            .IsRequired(false);

        builder.HasMany(e => e.Options)
            .WithMany(e => e.VehicleDetails);

        builder.Property(e => e.EngineVolume)
            .IsRequired(false);

        builder.Property(e => e.Mileage)
            .IsRequired();

        builder.Property(e => e.MileageType)
            .IsRequired();
    }
}
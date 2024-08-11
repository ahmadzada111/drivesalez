using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.VehicleDetail)
            .WithOne(e => e.Vehicle)
            .HasForeignKey<VehicleDetail>(e => e.VehicleId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.Make)
            .WithMany(e => e.Vehicles)
            .HasForeignKey(e => e.MakeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Model)
            .WithMany(e => e.Vehicles)
            .HasForeignKey(e => e.ModelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Announcements)
            .WithOne(e => e.Vehicle)
            .HasForeignKey(e => e.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
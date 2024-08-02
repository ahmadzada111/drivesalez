using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class ManufactureYearConfiguration : IEntityTypeConfiguration<ManufactureYear>
{
    public void Configure(EntityTypeBuilder<ManufactureYear> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Year)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.Year)
            .HasForeignKey(e => e.YearId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
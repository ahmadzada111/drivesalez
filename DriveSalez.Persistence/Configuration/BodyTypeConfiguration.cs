using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class BodyTypeConfiguration : IEntityTypeConfiguration<BodyType>
{
    public void Configure(EntityTypeBuilder<BodyType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithOne(e => e.BodyType)
            .HasForeignKey(e => e.BodyTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
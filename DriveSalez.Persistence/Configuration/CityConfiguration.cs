using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasOne(e => e.Country)
            .WithMany(e => e.Cities)
            .HasForeignKey(e => e.CountryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
            
        builder.HasMany(e => e.Announcements)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(e => e.Country)
            .WithMany(e => e.Cities)
            .HasForeignKey(e => e.CountryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Announcements)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(e => e.Cities)
            .WithOne(e => e.Country)
            .HasForeignKey(e => e.CountryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Announcements)
            .WithOne(e => e.Country)
            .HasForeignKey(e => e.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
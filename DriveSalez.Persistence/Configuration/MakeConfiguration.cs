using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class MakeConfiguration : IEntityTypeConfiguration<Make>
{
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(e => e.Models)
            .WithOne(e => e.Make)
            .HasForeignKey(e => e.MakeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Vehicles)
            .WithOne(e => e.Make)
            .HasForeignKey(e => e.MakeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
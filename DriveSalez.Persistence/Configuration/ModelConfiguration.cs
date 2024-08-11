using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasOne(e => e.Make)
            .WithMany(e => e.Models)
            .HasForeignKey(e => e.MakeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Vehicles)
            .WithOne(e => e.Model)
            .HasForeignKey(e => e.ModelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
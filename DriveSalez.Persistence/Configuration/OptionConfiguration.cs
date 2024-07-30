using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(e => e.VehicleDetails)
            .WithMany(e => e.Options);
    }
}
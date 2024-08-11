using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class PricingOptionConfiguration : IEntityTypeConfiguration<PricingOption>
{
    public void Configure(EntityTypeBuilder<PricingOption> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Price)
            .IsRequired();
    }
}
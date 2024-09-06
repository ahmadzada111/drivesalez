using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class OneTimePurchaseConfiguration : IEntityTypeConfiguration<OneTimePurchase>
{
    public void Configure(EntityTypeBuilder<OneTimePurchase> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LimitType)
            .IsRequired();

        builder.Property(e => e.LimitValue)
            .IsRequired();
        
        builder.Property(e => e.Price)
            .IsRequired();

        builder.HasOne(e => e.UserPurchase)
            .WithOne(e => e.OneTimePurchase)
            .HasForeignKey<UserPurchase>(e => e.OneTimePurchaseId)
            .IsRequired();
    }
}
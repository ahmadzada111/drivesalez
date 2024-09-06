using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class UserPurchaseConfiguration : IEntityTypeConfiguration<UserPurchase>
{
    public void Configure(EntityTypeBuilder<UserPurchase> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
            .WithMany(e => e.UserPurchases)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.HasOne(e => e.OneTimePurchase)
            .WithOne(e => e.UserPurchase)
            .HasForeignKey<UserPurchase>(e => e.OneTimePurchaseId)
            .IsRequired();

        builder.Property(e => e.PurchaseDate)
            .IsRequired();
    }
}
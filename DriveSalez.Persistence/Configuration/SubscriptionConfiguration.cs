using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Price)
            .IsRequired();

        builder.Property(e => e.DurationInDays)
            .IsRequired();

        builder.HasMany(e => e.UserSubscriptions)
            .WithOne(e => e.Subscription)
            .HasForeignKey(e => e.SubscriptionId)
            .IsRequired();
    }
}
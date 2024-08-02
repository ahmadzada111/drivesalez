using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(e => e.Price)
            .WithMany(e => e.Subscriptions)
            .HasForeignKey(e => e.PriceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
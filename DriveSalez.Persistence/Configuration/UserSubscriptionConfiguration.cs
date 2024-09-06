using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Subscription)
            .HasForeignKey<UserSubscription>(x => x.UserId)
            .IsRequired();
        
        builder.HasOne(x => x.Subscription)
            .WithMany(x => x.UserSubscriptions)
            .HasForeignKey(x => x.SubscriptionId)
            .IsRequired();
        
        builder.Property(e => e.StartDate)
            .IsRequired();
        
        builder.Property(e => e.ExpirationDate)
            .IsRequired();
    }
}
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasBaseType<BaseUser>();
        
        builder.Property(e => e.FirstName)
            .HasMaxLength(30)
            .IsRequired(false);
        
        builder.Property(e => e.LastName)
            .HasMaxLength(30)
            .IsRequired(false);
        
        builder.Property(e => e.Address)
            .HasMaxLength(250)
            .IsRequired(false);
        
        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.HasMany(e => e.WorkHours)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);

        builder.HasOne(e => e.Subscription)
            .WithOne(e => e.User)
            .HasForeignKey<UserSubscription>(e => e.UserId);
        
        builder.HasMany(e => e.PhoneNumbers)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(e => e.ProfilePhotoUrl)
            .WithOne(e => e.ProfilePhotoBusinessAccount)
            .HasForeignKey<ImageUrl>(e => e.ProfilePhotoUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(e => e.BackgroundPhotoUrl)
            .WithOne(e => e.BackgroundPhotoBusinessAccount)
            .HasForeignKey<ImageUrl>(e => e.BackgroundPhotoUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasMany(e => e.Announcements)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Property(e => e.AccountBalance)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0.00)
            .IsRequired();
    }
}
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class BusinessAccountConfiguration : IEntityTypeConfiguration<BusinessAccount>
{
    public void Configure(EntityTypeBuilder<BusinessAccount> builder)
    {
        builder.HasBaseType<ApplicationUser>();

        builder.Property(e => e.Address)
            .HasMaxLength(250)
            .IsRequired(false);
        
        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(e => e.WorkHours)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(e => e.PhoneNumbers)
            .WithOne(p => p.BusinessAccount)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

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
    }
}
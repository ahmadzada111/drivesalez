using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class BusinessAccountConfiguration : IEntityTypeConfiguration<BusinessAccount>
{
    public void Configure(EntityTypeBuilder<BusinessAccount> builder)
    {
        builder.HasBaseType<ApplicationUser>();

        builder.Property(e => e.Address)
            .HasMaxLength(250);
        
        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.WorkHours)
            .HasMaxLength(100);

         builder.HasOne(e => e.ProfilePhotoUrl)
            .WithOne(e => e.ProfilePhotoBusinessAccount)
            .HasForeignKey<ImageUrl>(e => e.ProfilePhotoUserId);

        builder.HasOne(e => e.BackgroundPhotoUrl)
            .WithOne(e => e.BackgroundPhotoBusinessAccount)
            .HasForeignKey<ImageUrl>(e => e.BackgroundPhotoUserId);
    }
}
using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasMany(e => e.PhoneNumbers)
            .WithOne(p => p.ApplicationUser)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Announcements)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(e => e.PremiumUploadLimit)
            .IsRequired();

        builder.Property(e => e.RegularUploadLimit)
            .IsRequired();

        builder.Property(e => e.CreationDate)
               .IsRequired();

        builder.Property(e => e.LastUpdateDate)
               .IsRequired();

        builder.Property(e => e.IsBanned)
               .IsRequired();
    }
}
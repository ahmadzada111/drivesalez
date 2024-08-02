using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
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

        builder.Property(e => e.RefreshToken)
            .IsRequired(false);

        builder.Property(e => e.RefreshTokenExpiration)
            .IsRequired(false);

        builder.HasMany(e => e.Announcements)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        
        builder.Property(e => e.PremiumUploadLimit)
            .IsRequired();

        builder.Property(e => e.RegularUploadLimit)
            .IsRequired();

        builder.Property(e => e.AccountBalance)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0.00)
            .IsRequired();

        builder.Property(e => e.CreationDate)
            .IsRequired();

        builder.Property(e => e.LastUpdateDate)
            .IsRequired(false);

        builder.Property(e => e.IsBanned)
            .IsRequired();
    }
}
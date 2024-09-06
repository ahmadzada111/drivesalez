using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class BaseUserConfiguration : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.ApplicationUser)
            .WithOne(e => e.BaseUser)
            .HasForeignKey<BaseUser>(e => e.IdentityId);
        
        builder.Property(e => e.FirstName)
            .HasMaxLength(30)
            .IsRequired(false);
        
        builder.Property(e => e.LastName)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(e => e.RefreshToken)
            .IsRequired(false);

        builder.Property(e => e.RefreshTokenExpiration)
            .IsRequired(false);

        builder.Property(e => e.CreationDate)
            .IsRequired();

        builder.Property(e => e.LastUpdateDate)
            .IsRequired(false);
    }
}
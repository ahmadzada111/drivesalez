using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.HasOne(e => e.BaseUser)
            .WithOne(e => e.ApplicationUser)
            .HasForeignKey<BaseUser>(e => e.IdentityId);
        
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(30)
            .IsRequired(false);
        
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(e => e.UserName)
            .IsRequired(false)
            .HasMaxLength(30);
    }
}
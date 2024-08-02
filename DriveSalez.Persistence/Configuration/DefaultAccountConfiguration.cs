using DriveSalez.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class DefaultAccountConfiguration : IEntityTypeConfiguration<DefaultAccount>
{
    public void Configure(EntityTypeBuilder<DefaultAccount> builder)
    {
        builder.HasBaseType<ApplicationUser>();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();
    }
}
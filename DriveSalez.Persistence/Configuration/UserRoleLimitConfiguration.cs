using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class UserRoleLimitConfiguration : IEntityTypeConfiguration<UserRoleLimit>
{
    public void Configure(EntityTypeBuilder<UserRoleLimit> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(l => l.Role)
            .WithOne(r => r.UserRoleLimit)
            .HasForeignKey<UserRoleLimit>(r => r.RoleId);
        
        builder.Property(x => x.Value)
            .IsRequired();
    }
}
using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class UserLimitConfiguration : IEntityTypeConfiguration<UserLimit>
{
    public void Configure(EntityTypeBuilder<UserLimit> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
            .WithMany(u => u.UserLimits)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.LimitType)
            .IsRequired();

        builder.Property(e => e.LimitValue)
            .IsRequired();
        
        builder.Property(e => e.UsedValue)
            .IsRequired();
    }
}
using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class AccountLimitConfiguration : IEntityTypeConfiguration<AccountLimit>
{
    public void Configure(EntityTypeBuilder<AccountLimit> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.PremiumAnnouncementsLimit)
            .IsRequired();

        builder.Property(e => e.RegularAnnouncementsLimit)
            .IsRequired();

        builder.Property(e => e.UserType)
            .IsRequired();
    }
}
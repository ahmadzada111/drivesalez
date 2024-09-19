using DriveSalez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

internal class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Number)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(e => e.PhoneNumbers)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
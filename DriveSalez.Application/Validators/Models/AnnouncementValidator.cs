using DriveSalez.Domain.Entities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class AnnouncementValidator : AbstractValidator<Announcement>
{
    public AnnouncementValidator()
    {
        RuleFor(announcement => announcement.Id)
                .NotEqual(Guid.Empty).WithMessage("ID must be a valid GUID.");

        RuleFor(announcement => announcement.VehicleId)
                .GreaterThan(0).When(announcement => announcement.VehicleId.HasValue)
                .WithMessage("Vehicle ID must be a positive integer.");
        
        RuleFor(announcement => announcement.Barter)
                .NotNull().WithMessage("Barter should be a boolean value.")
                .When(announcement => announcement.Barter.HasValue);
                
            RuleFor(announcement => announcement.OnCredit)
                .NotNull().WithMessage("On Credit should be a boolean value.")
                .When(announcement => announcement.OnCredit.HasValue);

            RuleFor(announcement => announcement.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(announcement => !string.IsNullOrEmpty(announcement.Description));

            RuleFor(announcement => announcement.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

            RuleFor(announcement => announcement.AnnouncementState)
                .IsInEnum().WithMessage("Announcement State must be a valid enum value.");

            RuleForEach(announcement => announcement.ImageUrls)
                .SetValidator(new ImageUrlValidator());

            RuleFor(announcement => announcement.CountryId)
                .GreaterThan(0).When(announcement => announcement.CountryId.HasValue)
                .WithMessage("Country ID must be a positive integer.");
                
            RuleFor(announcement => announcement.CityId)
                .GreaterThan(0).When(announcement => announcement.CityId.HasValue)
                .WithMessage("City ID must be a positive integer.");

            RuleFor(announcement => announcement.UserId)
                .NotEqual(Guid.Empty).WithMessage("User ID must be a valid GUID.");

            RuleFor(announcement => announcement.Owner)
                .NotNull().WithMessage("Owner reference is required.");

            RuleFor(announcement => announcement.ExpirationDate)
                .GreaterThanOrEqualTo(DateTimeOffset.Now).WithMessage("Expiration Date cannot be in the past.");
            
            RuleFor(announcement => announcement.PremiumExpirationDate)
                .GreaterThanOrEqualTo(DateTimeOffset.Now).WithMessage("Premium Expiration Date cannot be in the past.");
            
            RuleFor(announcement => announcement.ViewCount)
                .GreaterThanOrEqualTo(0).WithMessage("View Count cannot be negative.");
    }
}
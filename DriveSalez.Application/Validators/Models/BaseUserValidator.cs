using DriveSalez.Domain.IdentityEntities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class BaseUserValidator : AbstractValidator<BaseUser>
{
    public BaseUserValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(user => user.IdentityId)
            .NotEmpty().WithMessage("Identity ID is required.");

        RuleFor(user => user.FirstName)
            .Length(2, 50).WithMessage("First Name must be between 2 and 50 characters.")
            .When(user => !string.IsNullOrEmpty(user.FirstName));

        RuleFor(user => user.LastName)
            .Length(2, 50).WithMessage("Last Name must be between 2 and 50 characters.")
            .When(user => !string.IsNullOrEmpty(user.LastName));

        RuleFor(user => user.RefreshToken)
            .MaximumLength(100).WithMessage("Refresh Token cannot exceed 100 characters.")
            .When(user => !string.IsNullOrEmpty(user.RefreshToken));

        RuleFor(user => user.RefreshTokenExpiration)
            .GreaterThan(DateTime.Now).WithMessage("Refresh Token Expiration must be in the future.")
            .When(user => user.RefreshTokenExpiration.HasValue);

        RuleFor(user => user.CreationDate)
            .LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("Creation Date cannot be in the future.");

        RuleFor(user => user.LastUpdateDate)
            .GreaterThanOrEqualTo(user => user.CreationDate)
            .WithMessage("Last Update Date cannot be before Creation Date.")
            .When(user => user.LastUpdateDate.HasValue);
    }
}
using DriveSalez.Domain.Entities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class UserLimitValidator : AbstractValidator<UserLimit>
{
    public UserLimitValidator()
    {
        RuleFor(limit => limit.Id)
            .GreaterThan(0).WithMessage("ID must be a positive integer.");

        RuleFor(limit => limit.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(limit => limit.User)
            .NotNull().WithMessage("User reference is required.");

        RuleFor(limit => limit.LimitType)
            .IsInEnum().WithMessage("Limit Type must be a valid enum value.");

        RuleFor(limit => limit.LimitValue)
            .GreaterThanOrEqualTo(0).WithMessage("Limit Value cannot be negative.");

        RuleFor(limit => limit.UsedValue)
            .GreaterThanOrEqualTo(0).WithMessage("Used Value cannot be negative.")
            .LessThanOrEqualTo(limit => limit.LimitValue).WithMessage("Used Value cannot exceed Limit Value.");
    }
}
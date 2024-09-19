using DriveSalez.Domain.Entities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class PhoneNumberValidator : AbstractValidator<PhoneNumber>
{
    public PhoneNumberValidator()
    {
        RuleFor(phone => phone.Id)
            .GreaterThan(0).WithMessage("ID must be a positive integer.");

        RuleFor(phone => phone.Number)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid international format.");

        RuleFor(phone => phone.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(phone => phone.User)
            .NotNull().WithMessage("User reference is required.");
    }
}
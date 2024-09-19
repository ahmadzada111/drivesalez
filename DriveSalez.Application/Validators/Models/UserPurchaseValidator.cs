using DriveSalez.Domain.Entities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class UserPurchaseValidator : AbstractValidator<UserPurchase>
{
    public UserPurchaseValidator()
    {
        RuleFor(purchase => purchase.Id)
            .GreaterThan(0).WithMessage("ID must be a positive integer.");

        RuleFor(purchase => purchase.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(purchase => purchase.User)
            .NotNull().WithMessage("User reference is required.");

        RuleFor(purchase => purchase.OneTimePurchaseId)
            .GreaterThan(0).WithMessage("One Time Purchase ID must be a positive integer.");

        RuleFor(purchase => purchase.OneTimePurchase)
            .NotNull().WithMessage("One Time Purchase reference is required.");

        RuleFor(purchase => purchase.PurchaseDate)
            .LessThanOrEqualTo(DateTimeOffset.Now).WithMessage("Purchase Date cannot be in the future.");
    }
}
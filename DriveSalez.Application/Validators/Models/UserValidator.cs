using DriveSalez.Domain.IdentityEntities;
using FluentValidation;

namespace DriveSalez.Application.Validators.Models;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        Include(new BaseUserValidator());
        
        RuleFor(user => user.AccountBalance)
                .GreaterThanOrEqualTo(0).WithMessage("Account Balance cannot be negative.");

        RuleFor(user => user.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(user => !string.IsNullOrEmpty(user.Description));

        RuleFor(user => user.Address)
            .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.")
            .When(user => !string.IsNullOrEmpty(user.Address));

        RuleForEach(user => user.PhoneNumbers)
            .SetValidator(new PhoneNumberValidator());
        
        RuleForEach(user => user.Announcements)
            .SetValidator(new AnnouncementValidator());

        RuleForEach(user => user.UserLimits)
            .SetValidator(new UserLimitValidator());

        RuleForEach(user => user.UserPurchases)
            .SetValidator(new UserPurchaseValidator());

        RuleForEach(user => user.WorkHours)
            .SetValidator(new WorkHourValidator());
    }
}
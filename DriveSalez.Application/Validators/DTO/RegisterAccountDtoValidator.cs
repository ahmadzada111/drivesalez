using DriveSalez.SharedKernel.DTO.UserDTO;
using FluentValidation;

namespace DriveSalez.Application.Validators.DTO;

public class RegisterAccountDtoValidator : AbstractValidator<RegisterAccountDto>
{
    public RegisterAccountDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

        RuleFor(x => x.UserType)
            .NotEmpty().WithMessage("User Type is required.")
            .Must(BeAValidUserType).WithMessage("Invalid User Type.");

        RuleFor(x => x.PhoneNumbers)
            .Must(phoneNumbers => phoneNumbers == null || phoneNumbers.Count > 0).WithMessage("At least one phone number is required if phone numbers are provided.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.");
    }

    private bool BeAValidUserType(string userType)
    {
        var validUserTypes = new[] { "Default", "Business" };
        return validUserTypes.Contains(userType, StringComparer.OrdinalIgnoreCase);
    }
}
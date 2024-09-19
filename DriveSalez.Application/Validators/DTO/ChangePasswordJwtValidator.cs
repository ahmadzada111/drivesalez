using DriveSalez.SharedKernel.DTO.UserDTO;
using FluentValidation;

namespace DriveSalez.Application.Validators.DTO;

public class ChangePasswordJwtValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordJwtValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old password is required.")
            .MinimumLength(8).WithMessage("Old password must be at least 8 characters long.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.NewPassword).WithMessage("New password and confirm password do not match.");
    }
}
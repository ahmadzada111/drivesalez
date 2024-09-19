using DriveSalez.SharedKernel.DTO;
using FluentValidation;

namespace DriveSalez.Application.Validators.DTO;

public class ChangeEmailConfirmationDtoValidator : AbstractValidator<ChangeEmailConfirmationDto>
{
    public ChangeEmailConfirmationDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.NewEmail)
            .NotEmpty().WithMessage("New email is required.")
            .EmailAddress().WithMessage("New email is not in a valid format.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
    }
}
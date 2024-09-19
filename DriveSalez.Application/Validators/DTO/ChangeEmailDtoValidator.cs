using DriveSalez.SharedKernel.DTO.UserDTO;
using FluentValidation;

namespace DriveSalez.Application.Validators.DTO;

public class ChangeEmailDtoValidator : AbstractValidator<ChangeEmailDto>
{
    public ChangeEmailDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Current email is required.")
            .EmailAddress().WithMessage("Current email is not in a valid format.");

        RuleFor(x => x.NewEmail)
            .NotEmpty().WithMessage("New email is required.")
            .EmailAddress().WithMessage("New email is not in a valid format.");
    }
}
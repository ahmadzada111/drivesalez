using DriveSalez.SharedKernel.DTO;
using FluentValidation;

namespace DriveSalez.Application.Validators.DTO;

public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
{
    public ConfirmEmailDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
    }
}
using DriveSalez.SharedKernel.DTO;
using FluentValidation;

namespace DriveSalez.Application.Validators.DTO;

public class RefreshJwtDtoValidator : AbstractValidator<RefreshJwtDto>
{
    public RefreshJwtDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token is required.");
    }
}
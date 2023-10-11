using DriveSalez.Core.DTO;
using FluentValidation;

namespace DriveSalez.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(e => e.Email).EmailAddress().NotEmpty();
        RuleFor(e=>e.Password).MinimumLength(8).NotEmpty();
    }
}

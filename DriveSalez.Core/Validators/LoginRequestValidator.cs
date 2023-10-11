using DriveSalez.Core.DTO;
using FluentValidation;

namespace DriveSalez.Core.Validators;

public class LoginRequestValidator : AbstractValidator<RegisterDto>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.Email).EmailAddress().NotEmpty();
    }
}

using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public record ValidateOtpDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }
    
    [Required(ErrorMessage = "OTP cannot be blank!")]
    public int Otp { get; init; }
}
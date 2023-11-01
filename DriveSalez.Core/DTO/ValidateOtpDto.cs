using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Core.DTO;

public class ValidateOtpDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "OTP cannot be blank!")]
    public string Otp { get; set; }
}
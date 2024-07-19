using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public class ResetPasswordDto
{
    public ValidateOtpDto ValidateRequest { get; set; }
    
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string NewPassword { get; set; }
        
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
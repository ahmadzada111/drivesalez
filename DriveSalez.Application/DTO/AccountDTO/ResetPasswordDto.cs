using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO.AccountDTO;

public record ResetPasswordDto
{
    public ValidateOtpDto ValidateRequest { get; init; }
    
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string NewPassword { get; init; }
        
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; init; }
}
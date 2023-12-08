using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Core.DTO;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Old password cannot be blank!")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }
    
    [Required(ErrorMessage = "New password cannot be blank!")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string NewPassword { get; set; }
    
    [Required(ErrorMessage = "Confirm password cannot be blank!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO.AccountDTO;

public record ChangePasswordDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }
    
    [Required(ErrorMessage = "Old password cannot be blank!")]
    [DataType(DataType.Password)]
    public string OldPassword { get; init; }
    
    [Required(ErrorMessage = "New password cannot be blank!")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string NewPassword { get; init; }
    
    [Required(ErrorMessage = "Confirm password cannot be blank!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public record RegisterBusinessAccountDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }
    
    [Required(ErrorMessage = "Company name cannot be blank!")]
    public string UserName { get; init; }
    
    [Required(ErrorMessage = "Address name cannot be blank!")]
    public string Address { get; init; }

    [Required(ErrorMessage = "Description name cannot be blank!")]
    public string Description { get; init; }
    
    [Required(ErrorMessage = "Work hours cannot be blank!")]
    public string WorkHours { get; init; }
    
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; init; }
        
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; init; }
    
    [Required(ErrorMessage = "Phone number cannot be blank!")]
    [DataType(DataType.PhoneNumber)]
    public List<string> PhoneNumbers { get; init; }
}
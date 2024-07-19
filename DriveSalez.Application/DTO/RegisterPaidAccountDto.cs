using System.ComponentModel.DataAnnotations;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.DTO;

public class RegisterPaidAccountDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Company name cannot be blank!")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Address name cannot be blank!")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Description name cannot be blank!")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Work hours cannot be blank!")]
    public string WorkHours { get; set; }
    
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; }
        
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Phone number cannot be blank!")]
    [DataType(DataType.PhoneNumber)]
    public List<AccountPhoneNumber> PhoneNumbers { get; set; }
}
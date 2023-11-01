using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Core.DTO;

public class RegisterModeratorDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Person name cannot be blank!")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Person surname cannot be blank!")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
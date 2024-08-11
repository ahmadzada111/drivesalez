using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public record RegisterModeratorDto
{
    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }
    
    [Required(ErrorMessage = "Person name cannot be blank!")]
    public string FirstName { get; init; }
    
    [Required(ErrorMessage = "Person surname cannot be blank!")]
    public string LastName { get; init; }
    
    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}
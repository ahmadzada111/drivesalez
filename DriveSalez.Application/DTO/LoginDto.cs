using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public class LoginDto
{
    [Required(ErrorMessage = "User name cannot be blank!")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
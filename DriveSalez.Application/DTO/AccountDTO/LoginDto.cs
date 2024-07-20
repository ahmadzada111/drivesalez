using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO.AccountDTO;

public record LoginDto
{
    [Required(ErrorMessage = "User name cannot be blank!")]
    public string UserName { get; init; }

    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}
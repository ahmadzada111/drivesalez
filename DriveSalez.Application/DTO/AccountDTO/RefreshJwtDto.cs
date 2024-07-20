using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO.AccountDTO;

public record RefreshJwtDto
{
    [Required(ErrorMessage = "JWT token cannot be blank!")]
    public string? Token { get; init; }
    
    [Required(ErrorMessage = "Refresh token cannot be blank!")]
    public string? RefreshToken { get; init; }
}
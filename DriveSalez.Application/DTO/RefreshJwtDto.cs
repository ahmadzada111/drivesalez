using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public class RefreshJwtDto
{
    [Required(ErrorMessage = "JWT token cannot be blank!")]
    public string? Token { get; set; }
    
    [Required(ErrorMessage = "Refresh token cannot be blank!")]
    public string? RefreshToken { get; set; }
}
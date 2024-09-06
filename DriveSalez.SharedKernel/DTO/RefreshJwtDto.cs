namespace DriveSalez.SharedKernel.DTO;

public record RefreshJwtDto
{
    public string Token { get; init; }
    
    public string RefreshToken { get; init; }
}
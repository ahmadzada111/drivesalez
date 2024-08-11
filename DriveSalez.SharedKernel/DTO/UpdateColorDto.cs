namespace DriveSalez.Application.DTO;

public record UpdateColorDto
{
    public int ColorId { get; init; }
    
    public string NewColor { get; init; }
}
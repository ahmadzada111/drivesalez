namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateColorDto
{
    public int ColorId { get; init; }
    
    public string NewColor { get; init; }
}
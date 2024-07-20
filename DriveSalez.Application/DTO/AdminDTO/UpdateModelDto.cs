namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateModelDto
{
    public int ModelId { get; init; }
    
    public string NewModel { get; init; }
}
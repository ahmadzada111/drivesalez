namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateMakeDto
{
    public int MakeId { get; init; } 
    
    public string NewMake { get; init; }
}
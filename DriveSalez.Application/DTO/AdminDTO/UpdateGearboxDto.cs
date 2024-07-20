namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateGearboxDto
{
    public int GearboxId { get; init; }
    
    public string NewGearbox { get; init; }
}
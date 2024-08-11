namespace DriveSalez.Application.DTO;

public record UpdateOptionDto
{
    public int OptionId { get; init; }
    
    public string NewVehicleOption { get; init; }
}
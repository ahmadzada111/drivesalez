namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateOptionDto
{
    public int OptionId { get; init; }
    
    public string NewVehicleOption { get; init; }
}
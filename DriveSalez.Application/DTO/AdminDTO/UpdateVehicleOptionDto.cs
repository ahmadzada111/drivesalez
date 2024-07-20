namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateVehicleOptionDto
{
    public int VehicleOptionId { get; init; }
    
    public string NewVehicleOption { get; init; }
}
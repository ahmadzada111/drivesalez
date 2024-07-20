namespace DriveSalez.Application.DTO;

public record UpdateFuelTypeDto
{
    public int FuelTypeId { get; init; }
    
    public string NewFuelType { get; init; }
}
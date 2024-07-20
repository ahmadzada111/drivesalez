namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateVehicleConditionDto
{
    public int VehicleConditionId { get; init; } 
    
    public string NewVehicleCondition { get; init; }
    
    public string NewDescription { get; init; }
}
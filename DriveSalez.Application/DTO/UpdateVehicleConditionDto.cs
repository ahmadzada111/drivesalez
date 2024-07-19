namespace DriveSalez.Application.DTO;

public class UpdateVehicleConditionDto
{
    public int VehicleConditionId { get; set; } 
    
    public string NewVehicleCondition { get; set; }
    
    public string NewDescription { get; set; }
}
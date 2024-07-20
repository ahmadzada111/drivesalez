using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class VehicleFuelType
{
    public int Id { get; set; }

    public string FuelType { get; set; }
    
    public List<Vehicle> Vehicles { get; set; }       
}
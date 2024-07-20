using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class VehicleBodyType
{
    public int Id { get; set; }

    public string BodyType { get; set; }
    
    public List<VehicleDetails> VehicleDetails { get; set; }        
}
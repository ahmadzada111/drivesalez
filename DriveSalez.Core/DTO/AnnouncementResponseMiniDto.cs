using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO;

public class AnnouncementResponseMiniDto
{
    public Guid Id { get; set; }
    
    public Make Make { get; set; }
    
    public Model Model { get; set; }
    
    public decimal Price { get; set; }
    
    public int Mileage { get; set; }
    
    public string MileageType { get; set; }
    
    public double EngineVolume { get; set; }
    
    public VehicleFuelType FuelType { get; set; }
    
    public ManufactureYear Year { get; set; }
    
    public Currency Currency { get; set; }
    
    public ImageUrl ImageUrl { get; set; }
}
using System.ComponentModel.DataAnnotations;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class VehicleDetails
{
    [Key]
    public int Id { get; set; }

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Car Make can't be longer than 30 characters or less than 3.")]
    public VehicleBodyType BodyType { get; set; }     

    public VehicleColor Color { get; set; }       

    public int HorsePower { get; set; }     

    public VehicleGearboxType GearboxType { get; set; }     

    public VehicleDrivetrainType DrivetrainType { get; set; }  

    public List<VehicleCondition> Conditions { get; set; }  

    public VehicleMarketVersion MarketVersion { get; set; }      

    public int? OwnerQuantity { get; set; }

    public int? SeatCount { get; set; }

    public string? VinCode { get; set; }

    public List<VehicleOption> Options { get; set; }      

    public int? EngineVolume { get; set; }     

    public int MileAge { get; set; }    

    public DistanceUnit MileageType { get; set; }
}
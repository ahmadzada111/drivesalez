using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class VehicleDetail
{
    public int Id { get; set; }

    [JsonIgnore]
    public int FuelTypeId { get; set; }

    public FuelType FuelType { get; set; }

    [JsonIgnore]
    public int YearId { get; set; }

    public ManufactureYear Year { get; set; }   

    [JsonIgnore]
    public int BodyTypeId { get; set; }

    public BodyType BodyType { get; set; }     

    [JsonIgnore]
    public int ColorId { get; set; }

    public Color Color { get; set; }       

    public bool? IsBrandNew { get; set; }      

    public int HorsePower { get; set; }     

    [JsonIgnore]
    public int GearboxTypeId { get; set; }

    public GearboxType GearboxType { get; set; }     

    [JsonIgnore]
    public int DrivetrainTypeId { get; set; }  

    public DrivetrainType DrivetrainType { get; set; }  

    public List<Condition> Conditions { get; set; }  

    [JsonIgnore]
    public int MarketVersionId { get; set; }  

    public MarketVersion MarketVersion { get; set; }      

    public int? OwnerQuantity { get; set; }

    public int? SeatCount { get; set; }

    public string? VinCode { get; set; }

    public List<Option> Options { get; set; }      

    public int? EngineVolume { get; set; }     

    public int Mileage { get; set; }    

    public DistanceUnit MileageType { get; set; }
}
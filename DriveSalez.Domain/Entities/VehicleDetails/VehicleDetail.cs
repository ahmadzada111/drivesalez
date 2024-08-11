using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class VehicleDetail
{
    public int Id { get; set; }

    public int FuelTypeId { get; set; }

    public FuelType FuelType { get; set; }

    public int YearId { get; set; }

    public ManufactureYear Year { get; set; }   

    public int BodyTypeId { get; set; }

    public BodyType BodyType { get; set; }     

    public int ColorId { get; set; }

    public Color Color { get; set; }       

    public bool? IsBrandNew { get; set; }      

    public int HorsePower { get; set; }     

    public int GearboxTypeId { get; set; }

    public GearboxType GearboxType { get; set; }     

    public int DrivetrainTypeId { get; set; }  

    public DrivetrainType DrivetrainType { get; set; }  

    public List<Condition> Conditions { get; } = []; 

    public int MarketVersionId { get; set; }  

    public MarketVersion MarketVersion { get; set; }      

    public int? OwnerQuantity { get; set; }

    public int? SeatCount { get; set; }

    public string? VinCode { get; set; }

    public List<Option> Options { get; } = [];      

    public int? EngineVolume { get; set; }     

    public int Mileage { get; set; }    

    public int VehicleId { get; set; }

    public Vehicle Vehicle { get; set; }

    public DistanceUnit MileageType { get; set; }

    public VehicleDetail() { }

    public VehicleDetail(ManufactureYear manufactureYear, FuelType fuelType, bool isBrandNew, 
        BodyType bodyType, Color color, int horsePower, GearboxType gearboxType, DrivetrainType drivetrainType,
        MarketVersion marketVersion, int ownerQuantity, List<Option> options, List<Condition> conditions,
        int seatCount, string vinCode, int engineVolume, int mileage, DistanceUnit distanceUnit)
        {
            Year = manufactureYear;
            FuelType = fuelType;
            IsBrandNew = isBrandNew;
            BodyType = bodyType;
            Color = color;
            HorsePower = horsePower;
            GearboxType = gearboxType;
            DrivetrainType = drivetrainType;
            MarketVersion = marketVersion;
            OwnerQuantity = ownerQuantity;
            Options = options;
            Conditions = conditions;
            SeatCount = seatCount;
            VinCode = vinCode;
            EngineVolume = engineVolume;
            Mileage = mileage;
            MileageType = distanceUnit;
        }
}
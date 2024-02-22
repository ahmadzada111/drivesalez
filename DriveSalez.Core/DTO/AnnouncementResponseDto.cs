using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Domain.Entities.VehicleParts;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO;

public class AnnouncementResponseDto
{
    public Guid Id { get; set; }
    
    public ManufactureYear? Year { get; set; }   // 2009

    public Make? Make { get; set; }

    public Model? Model { get; set; }
    
    public VehicleFuelType? FuelType { get; set; }
    
    public VehicleBodyType BodyType { get; set; }    // Sedan,Pick-up, Truck, Roadster     

    public VehicleColor Color { get; set; }       // Red

    public int HorsePower { get; set; }     // 150 hp

    public VehicleGearboxType GearboxType { get; set; }     // Manual

    public VehicleDrivetrainType DrivetrainType { get; set; }  // FWD

    public List<VehicleCondition> Conditions { get; set; }  //No Damage, No Color

    public VehicleMarketVersion MarketVersion { get; set; }      // US

    public int? OwnerQuantity { get; set; }

    public int? SeatCount { get; set; }

    public string? VinCode { get; set; }

    public List<VehicleOption> Options { get; set; }      // ABS, REAR CAMERA, FRONT RADAR

    public int? EngineVolume { get; set; }     //6200 CC

    public int Mileage { get; set; }    //149000 km

    public string MileageType { get; set; }
    
    public bool? IsBrandNew { get; set; }       //NEW Or USED

    public bool? Barter { get; set; }

    public bool? OnCredit { get; set; }

    public string? Description { get; set; }
      
    public decimal Price { get; set; }    

    public bool IsPremium { get; set; }
    
    public Currency Currency { get; set; }

    public AnnouncementState AnnouncementState { get; set; }

    public List<ImageUrl>? ImageUrls { get; set; }
        
    public Country Country { get; set; }

    public City City { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }

    public int ViewCount { get; set; }
    
    public Guid UserId { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public List<AccountPhoneNumber> PhoneNumbers { get; set; }
}
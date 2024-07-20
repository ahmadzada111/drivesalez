using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.DTO;

public class AnnouncementResponseDto
{
    public Guid Id { get; set; }
    
    public ManufactureYear? Year { get; set; }   

    public Make? Make { get; set; }

    public Model? Model { get; set; }
    
    public VehicleFuelType? FuelType { get; set; }
    
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

    public int Mileage { get; set; }    

    public string MileageType { get; set; }
    
    public bool? IsBrandNew { get; set; }       

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
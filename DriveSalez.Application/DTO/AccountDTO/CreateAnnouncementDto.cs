using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.DTO.AccountDTO;

public record CreateAnnouncementDto
{
    [Required(ErrorMessage = "Year cannot be blank!")]
    public int? YearId { get; init; }   

    [Required(ErrorMessage = "Make cannot be blank!")]
    public int? MakeId { get; init; }

    [Required(ErrorMessage = "Model cannot be blank!")]
    public int? ModelId { get; init; }

    [Required(ErrorMessage = "Fuel type cannot be blank!")]
    public int? FuelTypeId { get; init; }

    [Required(ErrorMessage = "Gearbox be blank!")]
    public int? GearboxId { get; init; }

    [Required(ErrorMessage = "Drivetrain cannot be blank!")]
    public int? DrivetrainTypeId { get; init; }

    [Required(ErrorMessage = "Body type cannot be blank!")]
    public int? BodyTypeId { get; init; }
        
    [Required(ErrorMessage = "Conditions cannot be blank!")]
    public List<int>? ConditionsIds { get; init; }
        
    [Required(ErrorMessage = "Options cannot be blank!")]
    public List<int>? OptionsIds { get; init; }

    [Required(ErrorMessage = "Color cannot be blank!")]
    public int? ColorId { get; init; }

    [Required(ErrorMessage = "Market version cannot be blank!")]
    public int? MarketVersionId { get; init; }

    [Required(ErrorMessage = "Horse power cannot be blank!")]
    public int HorsePower { get; init; }     

    [Required(ErrorMessage = "Is brand new cannot be blank!")]
    public bool? IsBrandNew { get; init; }       

    [Required(ErrorMessage = "Owner quantity cannot be blank!")]
    public int? OwnerQuantity { get; init; }

    [Required(ErrorMessage = "Seat count cannot be blank!")]
    public int? SeatCount { get; init; }
        
    public string? VinCode { get; init; }

    [Required(ErrorMessage = "Mileage cannot be blank!")]
    public int Mileage { get; init; }    

    [Required(ErrorMessage = "Mileage type cannot be blank!")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DistanceUnit MileageType { get; init; }

    [Required(ErrorMessage = "Engine volume cannot be blank!")]
    public int? EngineVolume { get; init; }

    [Required(ErrorMessage = "Images cannot be blank!")]
    public List<string>? ImageData { get; init; }

    [Required(ErrorMessage = "Country cannot be blank!")]
    public int CountryId { get; init; }

    [Required(ErrorMessage = "City cannot be blank!")]
    public int CityId { get; init; }

    [Required(ErrorMessage = "Barter cannot be blank!")]
    public bool? Barter { get; init; }

    [Required(ErrorMessage = "On credit cannot be blank!")]
    public bool? OnCredit { get; init; }

    [Required(ErrorMessage = "Description cannot be blank!")]
    public string? Description { get; init; }

    [Required(ErrorMessage = "Price cannot be blank!")]
    public decimal Price { get; init; }   

    [Required(ErrorMessage = "Currency cannot be blank!")]
    public int CurrencyId { get; init; } 
        
    [Required(ErrorMessage = "Is Premium cannot be blank!")]
    public bool IsPremium { get; init; }
}
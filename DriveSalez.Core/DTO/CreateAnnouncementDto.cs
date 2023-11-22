using System.ComponentModel.DataAnnotations;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace DriveSalez.Core.DTO
{
    public class CreateAnnouncementDto
    {
        [Required(ErrorMessage = "Year cannot be blank!")]
        public int? YearId { get; set; }   

        [Required(ErrorMessage = "Make cannot be blank!")]
        public int? MakeId { get; set; }

        [Required(ErrorMessage = "Model cannot be blank!")]
        public int? ModelId { get; set; }

        [Required(ErrorMessage = "Fuel type cannot be blank!")]
        public int? FuelTypeId { get; set; }

        [Required(ErrorMessage = "Gearbox be blank!")]
        public int? GearboxId { get; set; }

        [Required(ErrorMessage = "Drivetrain cannot be blank!")]
        public int? DrivetrainTypeId { get; set; }

        [Required(ErrorMessage = "Body type cannot be blank!")]
        public int? BodyTypeId { get; set; }
        
        [Required(ErrorMessage = "Conditions cannot be blank!")]
        public List<int>? ConditionsIds { get; set; }
        
        [Required(ErrorMessage = "Options cannot be blank!")]
        public List<int>? OptionsIds { get; set; }

        [Required(ErrorMessage = "Color cannot be blank!")]
        public int? ColorId { get; set; }

        [Required(ErrorMessage = "Market version cannot be blank!")]
        public int? MarketVersionId { get; set; }

        [Required(ErrorMessage = "Horse power cannot be blank!")]
        public int HorsePower { get; set; }     

        [Required(ErrorMessage = "Is brand new cannot be blank!")]
        public bool? IsBrandNew { get; set; }       

        [Required(ErrorMessage = "Owner quantity cannot be blank!")]
        public int? OwnerQuantity { get; set; }

        [Required(ErrorMessage = "Seat count cannot be blank!")]
        public int? SeatCount { get; set; }

        [Required(ErrorMessage = "Vin code cannot be blank!")]
        public string? VinCode { get; set; }

        [Required(ErrorMessage = "Mileage cannot be blank!")]
        public int Mileage { get; set; }    

        [Required(ErrorMessage = "Mileage type cannot be blank!")]
        public DistanceUnit MileageType { get; set; }

        [Required(ErrorMessage = "Engine volume cannot be blank!")]
        public int? EngineVolume { get; set; }

        [Required(ErrorMessage = "Images cannot be blank!")]
        public List<string>? ImageData { get; set; }

        [Required(ErrorMessage = "Country cannot be blank!")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "City cannot be blank!")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Barter cannot be blank!")]
        public bool? Barter { get; set; }

        [Required(ErrorMessage = "On credit cannot be blank!")]
        public bool? OnCredit { get; set; }

        [Required(ErrorMessage = "Description cannot be blank!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price cannot be blank!")]
        public decimal Price { get; set; }   

        [Required(ErrorMessage = "Currency cannot be blank!")]
        public int CurrencyId { get; set; } 
        
        [Required(ErrorMessage = "Is Premium cannot be blank!")]
        public bool IsPremium { get; set; }

        public bool IsFreePremiumToggleSwitched { get; set; }
        
        public PaymentRequestDto PaymentRequest { get; set; }
    }
}

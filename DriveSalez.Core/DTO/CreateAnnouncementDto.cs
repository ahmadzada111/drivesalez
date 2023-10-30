using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO
{
    public class CreateAnnouncementDto
    {
        public int? YearId { get; set; }   

        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public int? FuelTypeId { get; set; }

        public int? GearboxId { get; set; }

        public int? DriveTrainTypeId { get; set; }

        public int? BodyTypeId { get; set; }

        public List<int>? ConditionsIds { get; set; }
        
        public List<int>? OptionsIds { get; set; }

        public int? ColorId { get; set; }

        public int? MarketVersionId { get; set; }

        public int HorsePower { get; set; }     

        public bool? IsBrandNew { get; set; }       

        public int? OwnerQuantity { get; set; }

        public int? SeatCount { get; set; }

        public string? VinCode { get; set; }

        public int MileAge { get; set; }    

        public DistanceUnit MileageType { get; set; }

        public int? EngineVolume { get; set; }

        public List<ImageUrl>? ImageUrls { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }   

        public int CurrencyId { get; set; }  
    }
}

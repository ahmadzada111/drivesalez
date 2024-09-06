namespace DriveSalez.SharedKernel.DTO.AnnouncementDTO;

public record UpdateAnnouncementDto
{
    public int YearId { get; init; }   

    public int MakeId { get; init; }

    public int ModelId { get; init; }

    public int FuelTypeId { get; set; }

    public int GearboxId { get; init; }

    public int DrivetrainTypeId { get; init; }

    public int BodyTypeId { get; init; }
        
    public List<int> ConditionsIds { get; init; }
        
    public List<int> OptionsIds { get; init; }

    public int ColorId { get; init; }

    public int MarketVersionId { get; init; }

    public int HorsePower { get; init; }     

    public bool IsBrandNew { get; init; }       

    public int OwnerQuantity { get; init; }

    public int SeatCount { get; init; }

    public string VinCode { get; init; }

    public int Mileage { get; init; }    

    public string DistanceUnit { get; init; }

    public int EngineVolume { get; init; }

    public int CountryId { get; init; }

    public int CityId { get; init; }

    public bool Barter { get; init; }

    public bool OnCredit { get; init; }

    public string Description { get; init; }

    public decimal Price { get; init; }
}
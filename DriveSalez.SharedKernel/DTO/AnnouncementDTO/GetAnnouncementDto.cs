namespace DriveSalez.SharedKernel.DTO.AnnouncementDTO;

public record GetAnnouncementDto
{
    public Guid Id { get; init; }
    
    public string Year { get; init; }   

    public string Make { get; init; }

    public string Model { get; init; }
    
    public string FuelType { get; init; }
    
    public string BodyType { get; init; }        

    public string Color { get; init; }       

    public int HorsePower { get; init; }     

    public string GearboxType { get; init; }     

    public string DrivetrainType { get; init; }  

    public List<string> Conditions { get; init; }  

    public string MarketVersion { get; init; }      

    public int? OwnerQuantity { get; init; }

    public int? SeatCount { get; init; }

    public string? VinCode { get; init; }

    public List<string> Options { get; init; }      

    public int? EngineVolume { get; init; }     

    public int Mileage { get; init; }    

    public string DistanceUnit { get; init; }
    
    public bool? IsBrandNew { get; init; }       

    public bool? Barter { get; init; }

    public bool? OnCredit { get; init; }

    public string? Description { get; init; }
      
    public decimal Price { get; init; }    

    public bool IsPremium { get; init; }
    
    public string AnnouncementState { get; init; }

    public List<string>? ImageUrls { get; init; }
        
    public string Country { get; init; }

    public string City { get; init; }

    public DateTimeOffset ExpirationDate { get; init; }

    public int ViewCount { get; init; }
    
    public Guid UserId { get; init; }
    
    public string UserName { get; init; }
    
    public string Email { get; init; }
    
    public string? FirstName { get; init; }
    
    public string? LastName { get; init; }
    
    public string? WorkHours { get; init; }
    
    public string? PhoneNumber { get; init; }

    public string? Address { get; init; }

    public string? ProfilePhotoUrl { get; init; }
    
    public List<string> PhoneNumbers { get; init; }
}
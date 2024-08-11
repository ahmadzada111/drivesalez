using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class Announcement
{
    public Guid Id { get; set; }

    public int? VehicleId { get; set; }

    public required Vehicle Vehicle { get; set; }

    public bool? Barter { get; set; }

    public bool? OnCredit { get; set; }

    public string? Description { get; set; }
      
    public decimal Price { get; set; }  

    public AnnouncementState AnnouncementState { get; set; } = AnnouncementState.Pending;

    public List<ImageUrl> ImageUrls { get; } = [];
        
    public int? CountryId { get; set; }
    
    public int? CityId { get; set; }
    
    public required Country Country { get; set; }

    public required City City { get; set; }

    public Guid UserId {get; set;}

    public required ApplicationUser Owner { get; set; }
        
    public DateTimeOffset ExpirationDate { get; set; }
        
    public DateTimeOffset PremiumExpirationDate { get; set; }
        
    public bool IsPremium { get; set; }
        
    public int ViewCount { get; set; }
}
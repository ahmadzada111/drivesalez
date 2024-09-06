using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class Announcement
{
    public Guid Id { get; set; }

    public int? VehicleId { get; set; }

    public Vehicle Vehicle { get; set; }

    public bool? Barter { get; set; }

    public bool? OnCredit { get; set; }

    public string? Description { get; set; }
      
    public decimal Price { get; set; }  

    public AnnouncementState AnnouncementState { get; set; }

    public ICollection<ImageUrl> ImageUrls { get; set; } = [];
        
    public int? CountryId { get; set; }
    
    public int? CityId { get; set; }
    
    public Country Country { get; set; }

    public City City { get; set; }

    public Guid UserId {get; set;}

    public UserProfile Owner { get; set; }
        
    public DateTimeOffset ExpirationDate { get; set; }
        
    public DateTimeOffset PremiumExpirationDate { get; set; }
        
    public bool IsPremium { get; set; }
        
    public int ViewCount { get; set; }
}
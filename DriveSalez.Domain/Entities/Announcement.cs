using System.ComponentModel.DataAnnotations;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class Announcement
{
    [Key]
    public Guid Id { get; set; }

    public Vehicle Vehicle { get; set; }

    public bool? Barter { get; set; }

    public bool? OnCredit { get; set; }

    public string? Description { get; set; }
      
    public decimal Price { get; set; }  

    public Currency Currency { get; set; }  

    public AnnouncementState AnnouncementState { get; set; } = AnnouncementState.Pending;

    public List<ImageUrl>? ImageUrls { get; set; }
        
    public Country Country { get; set; }

    public City City { get; set; }

    public ApplicationUser Owner { get; set; }
        
    public DateTimeOffset ExpirationDate { get; set; }
        
    public DateTimeOffset PremiumExpirationDate { get; set; }
        
    public bool IsPremium { get; set; }
        
    public int ViewCount { get; set; }
}
using DriveSalez.Core.Enums;
using System.ComponentModel.DataAnnotations;
using DriveSalez.Core.Domain.IdentityEntities;

namespace DriveSalez.Core.Domain.Entities
{
    public class Announcement
    {
        [Key]
        public Guid Id { get; set; }

        public Vehicle Vehicle { get; set; }

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public string? Description { get; set; }
      
        public decimal Price { get; set; }  // 10900      

        public Currency Currency { get; set; }  // USD

        public AnnouncementState AnnouncementState { get; set; } = AnnouncementState.Waiting;

        public List<ImageUrl>? ImageUrls { get; set; }
        
        public Country Country { get; set; }

        public City City { get; set; }

        public ApplicationUser Owner { get; set; }
        
        public DateTimeOffset ExpirationDate { get; set; }
        
        public DateTimeOffset PremiumExpirationDate { get; set; }
        
        public bool IsPremium { get; set; }
        
        public int ViewCount { get; set; }
    }
}

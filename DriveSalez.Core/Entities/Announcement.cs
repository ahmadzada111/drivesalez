using DriveSalez.Core.Enums;
using System.ComponentModel.DataAnnotations;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        public Vehicle Vehicle { get; set; }

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public string? Description { get; set; }
      
        public decimal Price { get; set; }  // 10900      

        public Currency Currency { get; set; }  // USD

        public AnnouncementState AnnoucementState { get; set; } = AnnouncementState.Waiting;

        public Country Country { get; set; }

        public City City { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}

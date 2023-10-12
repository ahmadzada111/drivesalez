using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities
{
    public class CarDealer : ApplicationUser
    {
        public ImageUrl BackgroundPhotoUrl { get; set; }

        public List<CarDealerPhoneNumbers> PhoneNumbers { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public string? WorkHours { get; set; }

        public bool? IsOfficial { get; set; }
    }
}

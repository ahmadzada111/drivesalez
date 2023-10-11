using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities
{
    public class CarDealer : ApplicationUser
    {
        public string ProfilePhotoUrl { get; set; }

        public string BackgroundPhotoUrl { get; set; }

        public List<CarDealerPhoneNumbers> PhoneNumbers { get; set; }

        public string? Adress { get; set; }

        public string? Description { get; set; }

        public string? WorkHours { get; set; }

        public bool? IsOfficial { get; set; }
    }
}

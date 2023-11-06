using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities
{
    public class BusinessAccount : PremiumAccount
    {
        public bool? IsOfficial { get; set; }
    }
}

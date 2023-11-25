using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities
{
    public class BusinessAccount : PaidUser
    {
        public bool? IsOfficial { get; set; }
    }
}

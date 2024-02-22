using DriveSalez.Core.Domain.IdentityEntities;

namespace DriveSalez.Core.Domain.Entities
{
    public class BusinessAccount : PaidUser
    {
        public bool? IsOfficial { get; set; }
    }
}

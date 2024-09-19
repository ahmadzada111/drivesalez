using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class UserRoleLimit
{
    public int Id { get; set; }
    
    public Guid RoleId { get; set; }
    
    public ApplicationRole Role { get; set; }
    
    public LimitType LimitType { get; set; }
    
    public int Value { get; set; }
}
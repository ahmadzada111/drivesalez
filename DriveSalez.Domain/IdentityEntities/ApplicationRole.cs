using DriveSalez.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Domain.IdentityEntities;

public class ApplicationRole : IdentityRole<Guid>
{
    public UserRoleLimit UserRoleLimit { get; set; }
}
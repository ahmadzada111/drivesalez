using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.RepositoryContracts;

public interface IAccountRepository
{
    Task AddLimitsToAccountInDbAsync(Guid userId, UserType userType);

    Task<ApplicationUser> FindUserByLoginInDbAsync(string login);

    Task<ApplicationUser> ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToPremiumInDbAsync(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToBusinessInDbAsync(ApplicationUser user);
}
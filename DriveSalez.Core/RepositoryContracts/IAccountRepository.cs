using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.RepositoryContracts;

public interface IAccountRepository
{
    Task AddPremiumLimitToPaidAccountInDbAsync(Guid userId, UserType userType);

    Task<ApplicationUser> FindUserByLoginInDbAsync(string login);

    Task<ApplicationUser> ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user);
}
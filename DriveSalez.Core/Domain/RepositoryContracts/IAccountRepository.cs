using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO.Enums;

namespace DriveSalez.Core.Domain.RepositoryContracts;

public interface IAccountRepository
{
    Task AddLimitsToAccountInDbAsync(ApplicationUser user, UserType userType);

    Task<ApplicationUser?> FindUserByLoginInDbAsync(string login);

    Task<ApplicationUser?> ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user);

    Task<ApplicationUser?> ChangeUserTypeToPremiumInDbAsync(ApplicationUser user);

    Task<ApplicationUser?> ChangeUserTypeToBusinessInDbAsync(ApplicationUser user);

    Task<ApplicationUser?> DeleteUserFromDbAsync(Guid userId);
}
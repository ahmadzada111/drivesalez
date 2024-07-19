using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IAccountRepository
{
    Task AddLimitsToAccountInDbAsync(ApplicationUser user, UserType userType);

    Task<ApplicationUser?> FindUserByLoginInDbAsync(string login);

    Task<ApplicationUser> ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToBusinessInDbAsync(ApplicationUser user);

    Task<ApplicationUser> DeleteUserFromDbAsync(ApplicationUser user);
}
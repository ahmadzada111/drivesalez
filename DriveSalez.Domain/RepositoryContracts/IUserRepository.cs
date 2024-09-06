using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserRepository : IGenericRepository<BaseUser>
{
    Task<T?> FindUserOfType<T>(Guid id) where T : BaseUser;
}
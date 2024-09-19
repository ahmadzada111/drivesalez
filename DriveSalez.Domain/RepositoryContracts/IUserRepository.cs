using System.Linq.Expressions;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserRepository : IGenericRepository<BaseUser>
{
    Task<T?> FindUserOfType<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : BaseUser;
}
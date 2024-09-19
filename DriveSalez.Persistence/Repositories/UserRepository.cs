using System.Linq.Expressions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal sealed class UserRepository : GenericRepository<BaseUser>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> FindUserOfType<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : BaseUser
    {
        IQueryable<T> query = DbContext.Set<T>().OfType<T>();
        
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.FirstOrDefaultAsync(where);
    }
}
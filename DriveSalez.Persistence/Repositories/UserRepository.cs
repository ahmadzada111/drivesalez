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

    public async Task<T?> FindUserOfType<T>(Guid id) where T : BaseUser
    {
        return await DbContext.Set<T>().OfType<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class UserRoleLimitRepository : GenericRepository<UserRoleLimit>, IUserRoleLimitRepository
{
    public UserRoleLimitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
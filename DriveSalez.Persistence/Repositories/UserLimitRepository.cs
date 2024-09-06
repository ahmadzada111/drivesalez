using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class UserLimitRepository : GenericRepository<UserLimit>, IUserLimitRepository
{
    public UserLimitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
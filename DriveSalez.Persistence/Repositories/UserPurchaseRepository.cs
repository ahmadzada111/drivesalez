using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class UserPurchaseRepository : GenericRepository<UserPurchase>, IUserPurchaseRepository
{
    public UserPurchaseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
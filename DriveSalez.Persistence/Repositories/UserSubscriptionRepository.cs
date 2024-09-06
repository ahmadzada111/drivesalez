using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class UserSubscriptionRepository : GenericRepository<UserSubscription>, IUserSubscriptionRepository
{
    public UserSubscriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
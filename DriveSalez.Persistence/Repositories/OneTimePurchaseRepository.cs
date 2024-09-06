using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class OneTimePurchaseRepository : GenericRepository<OneTimePurchase>, IOneTimePurchaseRepository
{
    public OneTimePurchaseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
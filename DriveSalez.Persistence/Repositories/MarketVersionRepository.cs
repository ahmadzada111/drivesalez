using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class MarketVersionRepository : GenericRepository<MarketVersion>, IMarketVersionRepository
{
    public MarketVersionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
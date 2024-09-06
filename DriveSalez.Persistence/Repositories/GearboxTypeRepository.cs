using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class GearboxTypeRepository : GenericRepository<GearboxType>, IGearboxTypeRepository
{
    public GearboxTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
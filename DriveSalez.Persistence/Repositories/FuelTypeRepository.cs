using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class FuelTypeRepository : GenericRepository<FuelType>, IFuelTypeRepository
{
    public FuelTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
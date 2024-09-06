using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class ManufactureYearRepository : GenericRepository<ManufactureYear>, IManufactureYearRepository
{
    public ManufactureYearRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
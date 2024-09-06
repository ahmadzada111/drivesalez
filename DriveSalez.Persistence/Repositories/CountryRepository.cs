using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
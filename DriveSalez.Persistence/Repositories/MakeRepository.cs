using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class MakeRepository : GenericRepository<Make>, IMakeRepository
{
    public MakeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
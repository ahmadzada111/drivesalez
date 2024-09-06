using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class BodyTypeRepository : GenericRepository<BodyType>, IBodyTypeRepository
{
    public BodyTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
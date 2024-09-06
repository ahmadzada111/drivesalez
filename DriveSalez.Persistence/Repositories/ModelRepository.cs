using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class ModelRepository : GenericRepository<Model>, IModelRepository
{
    public ModelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
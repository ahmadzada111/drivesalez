using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class ConditionRepository : GenericRepository<Condition>, IConditionRepository
{
    public ConditionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
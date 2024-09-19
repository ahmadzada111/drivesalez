using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class ColorRepository : GenericRepository<Color>, IColorRepository
{
    public ColorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
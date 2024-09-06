using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class DrivetrainTypeRepository : GenericRepository<DrivetrainType>, IDrivetrainTypeRepository
{
    public DrivetrainTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
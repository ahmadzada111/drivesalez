using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class VehicleDetailRepository : GenericRepository<VehicleDetail>, IVehicleDetailRepository
{
    public VehicleDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
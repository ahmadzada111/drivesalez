using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;

namespace DriveSalez.Infrastructure.Repositories;

public class DetailsRepository : IDetailsRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public DetailsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<VehicleColor> GetAllColorsFromDb()
    {
        return _dbContext.VehicleColors.ToList();
    }

    public IEnumerable<VehicleBodyType> GetAllVehicleBodyTypesFromDb()
    {
        return _dbContext.VehicleBodyTypes.ToList();
    }

    public IEnumerable<VehicleDrivetrainType> GetAllVehicleDrivetrainsFromDb()
    {
        return _dbContext.VehicleDriveTrainTypes.ToList();
    }

    public IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypesFromDb()
    {
        return _dbContext.VehicleGearboxTypes.ToList();
    }

    public IEnumerable<Make> GetAllMakesFromDb()
    {
        return _dbContext.Makes.ToList();
    }

    public IEnumerable<Model> GetAllModelsByMakeIdFromDb(int id)
    {
        return _dbContext.Models.Where(e => e.Make.Id == id).ToList();
    }

    public IEnumerable<VehicleFuelType> GetAllVehicleFuelTypesFromDb()
    {
        return _dbContext.VehicleFuelTypes.ToList();
    }

    public IEnumerable<VehicleCondition> GetAllVehicleDetailsConditionsFromDb()
    {
        return _dbContext.VehicleDetailsConditions.ToList();
    }

    public IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersionsFromDb()
    {
        return _dbContext.VehicleMarketVersions.ToList();
    }

    public IEnumerable<VehicleOption> GetAllVehicleDetailsOptionsFromDb()
    {
        return _dbContext.VehicleDetailsOptions.ToList();
    }
}
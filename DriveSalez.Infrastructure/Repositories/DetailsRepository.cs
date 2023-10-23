using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories;

public class DetailsRepository : IDetailsRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public DetailsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<VehicleColor>> GetAllColorsFromDb()
    {
        return await _dbContext.VehicleColors.ToListAsync();
    }

    public async Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesFromDb()
    {
        return await _dbContext.VehicleBodyTypes.ToListAsync();
    }

    public async Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsFromDb()
    {
        return await _dbContext.VehicleDriveTrainTypes.ToListAsync();
    }

    public async Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesFromDb()
    {
        return await _dbContext.VehicleGearboxTypes.ToListAsync();
    }

    public async Task<IEnumerable<Make>> GetAllMakesFromDb()
    {
        return await _dbContext.Makes.ToListAsync();
    }

    public async Task<IEnumerable<Model>> GetAllModelsByMakeIdFromDb(int id)
    {
        return await _dbContext.Models.Where(e => e.Make.Id == id).ToListAsync();
    }

    public async Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesFromDb()
    {
        return await _dbContext.VehicleFuelTypes.ToListAsync();
    }

    public async Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsFromDb()
    {
        return await _dbContext.VehicleDetailsConditions.ToListAsync();
    }

    public async Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsFromDb()
    {
        return await _dbContext.VehicleMarketVersions.ToListAsync();
    }

    public async Task<IEnumerable<Model>> GetAllModelsFromDb()
    {
        return await _dbContext.Models.Include(m => m.Make).ToListAsync();
    }
    
    public async Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsFromDb()
    {
        return await _dbContext.VehicleDetailsOptions.ToListAsync();
    }
    
    public async Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDb()
    {
        return await _dbContext.Years.ToListAsync();
    }
    
    public async Task<IEnumerable<Country>> GetAllCountriesFromDb()
    {
        return await _dbContext.Countries.ToListAsync();
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesFromDb()
    {
        return await _dbContext.Cities.Include(x=>x.Country).ToListAsync();
    }
}
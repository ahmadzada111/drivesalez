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

    public async Task<IEnumerable<VehicleColor>> GetAllColorsFromDbAsync()
    {
        return await _dbContext.VehicleColors.ToListAsync();
    }

    public async Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesFromDbAsync()
    {
        return await _dbContext.VehicleBodyTypes.ToListAsync();
    }
    
    public async Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsFromDbAsync()
    {
        return await _dbContext.VehicleDriveTrainTypes.ToListAsync();
    }

    public async Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesFromDbAsync()
    {
        return await _dbContext.VehicleGearboxTypes.ToListAsync();
    }

    public async Task<IEnumerable<Make>> GetAllMakesFromDbAsync()
    {
        return await _dbContext.Makes.ToListAsync();
    }

    public async Task<IEnumerable<Model>> GetAllModelsByMakeIdFromDbAsync(int id)
    {
        return await _dbContext.Models.Where(e => e.Make.Id == id).ToListAsync();
    }

    public async Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesFromDbAsync()
    {
        return await _dbContext.VehicleFuelTypes.ToListAsync();
    }

    public async Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsFromDbAsync()
    {
        return await _dbContext.VehicleDetailsConditions.ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsFromDbAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }
    
    public async Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsFromDbAsync()
    {
        return await _dbContext.VehicleMarketVersions.ToListAsync();
    }

    public async Task<IEnumerable<Model>> GetAllModelsFromDbAsync()
    {
        return await _dbContext.Models.Include(m => m.Make).ToListAsync();
    }
    
    public async Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsFromDbAsync()
    {
        return await _dbContext.VehicleDetailsOptions.ToListAsync();
    }
    
    public async Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDbAsync()
    {
        return await _dbContext.ManufactureYears.ToListAsync();
    }
    
    public async Task<IEnumerable<Country>> GetAllCountriesFromDbAsync()
    {
        return await _dbContext.Countries.ToListAsync();
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesFromDbAsync()
    {
        return await _dbContext.Cities.Include(x=>x.Country).ToListAsync();
    }
    
    public async Task<IEnumerable<Currency>> GetAllCurrenciesFromDbAsync()
    {
        return await _dbContext.Currencies.ToListAsync();
    }
}
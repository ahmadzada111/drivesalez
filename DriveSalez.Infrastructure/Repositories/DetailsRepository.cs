using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Domain.Entities.VehicleParts;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Infrastructure.Repositories;

public class DetailsRepository : IDetailsRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public DetailsRepository(ApplicationDbContext dbContext, ILogger<DetailsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<VehicleColor>> GetAllColorsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleColors from DB");
            
            return await _dbContext.VehicleColors.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleColors from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleBodyTypes from DB");
        
            return await _dbContext.VehicleBodyTypes.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleBodyTypes from DB: {e.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleDrivetrainTypes from DB");
        
            return await _dbContext.VehicleDriveTrainTypes.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleDrivetrainTypes from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleGearboxTypes from DB");
        
            return await _dbContext.VehicleGearboxTypes.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleGearboxTypes from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Make>> GetAllMakesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Makes from DB");
        
            return await _dbContext.Makes.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Makes from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Model>> GetAllModelsByMakeIdFromDbAsync(int id)
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Models by Make Id from DB");
        
            return await _dbContext.Models.Where(e => e.Make.Id == id).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Models by Make Id from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleFuelTypes from DB");
        
            return await _dbContext.VehicleFuelTypes.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleFuelTypes from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleConditions from DB");
        
            return await _dbContext.VehicleDetailsConditions.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleConditions from DB: {e.Message}");
            throw;
        }

    }

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Subscriptions from DB");
        
            return await _dbContext.Subscriptions
                .Include(x => x.Price)
                .ThenInclude(x => x.Currency)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Subscriptions from DB: {e.Message}");
            throw;
        }
       
    }

    public async Task<IEnumerable<AnnouncementTypePricing>> GetAllAnnouncementTypePricingsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting AnnouncementTypePricings from DB");

            return await _dbContext.AnnouncementPricing
                .Include(x => x.Price)
                .ThenInclude(x => x.Currency)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting AnnouncementTypePricings from DB: {e.Message}");
            throw;
        }
        
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesByCountryIdFromDbAsync(int countryId)
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Cities by Country Id from DB");
        
            return await _dbContext.Cities
                .Where(x => x.Country.Id == countryId)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Cities by Country Id from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleMarketVersions from DB");

            return await _dbContext.VehicleMarketVersions.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleMarketVersions from DB: {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Model>> GetAllModelsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Models from DB");

            return await _dbContext.Models.Include(m => m.Make).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Models from DB: {e.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting VehicleOptions from DB");

            return await _dbContext.VehicleDetailsOptions.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting VehicleOptions from DB: {e.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting ManufactureYears from DB");

            return await _dbContext.ManufactureYears.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting ManufactureYears from DB: {e.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<Country>> GetAllCountriesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Countries from DB");

            return await _dbContext.Countries.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Countries from DB: {e.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Cities from DB");

            return await _dbContext.Cities.Include(x=>x.Country).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Cities from DB: {e.Message}");
            throw;
        }
    }
    
    public async Task<IEnumerable<Currency>> GetAllCurrenciesFromDbAsync()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToLongTimeString()}] Getting Currencies from DB");
        
            return await _dbContext.Currencies.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting Currencies from DB: {e.Message}");
            throw;
        }
    }
}
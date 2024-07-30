using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
        
    public AdminRepository(ApplicationDbContext dbContext, ILogger<AdminRepository> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Color> SendNewColorToDbAsync(string color)
    {
        try
        {
            _logger.LogInformation($"Sending new color to DB");

            ArgumentException.ThrowIfNullOrEmpty(color);

            var response = await _dbContext.VehicleColors.AddAsync(new Color() { Title = color });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending new color to DB");
            throw;
        }
    }

    public async Task<Country> SendNewCountryToDbAsync(string country)
    {
        try
        {
            _logger.LogInformation("Sending new country to DB");

            ArgumentException.ThrowIfNullOrEmpty(country);

            var response = await _dbContext.Countries.AddAsync(new Country() { Name = country });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new country to DB");
            throw;
        }
    }
        
    public async Task<BodyType> SendNewBodyTypeToDbAsync(string bodyType)
    {
        try
        {
            _logger.LogInformation($"Sending new body type to DB");

            ArgumentException.ThrowIfNullOrEmpty(bodyType);

            var response = await _dbContext.VehicleBodyTypes.AddAsync(new BodyType() { Type = bodyType });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error sending new body type to DB");
            throw;
        }
    }

    public async Task<DrivetrainType> SendNewVehicleDrivetrainTypeToDbAsync(string driveTrainType)
    {
        try
        {
            _logger.LogInformation($"Sending new vehicle drivetrain type to DB");

            ArgumentException.ThrowIfNullOrEmpty(driveTrainType);

            var response = await _dbContext.VehicleDriveTrainTypes.AddAsync(new DrivetrainType() { Type = driveTrainType });
           
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error sending new vehicle drivetrain type to DB");
            throw;
        }
    }

    public async Task<GearboxType> SendNewVehicleGearboxTypeToDbAsync(string gearboxType)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle gearbox type to DB");

            ArgumentException.ThrowIfNullOrEmpty(gearboxType);

            var response = await _dbContext.VehicleGearboxTypes.AddAsync(new GearboxType() { Type = gearboxType });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle gearbox type to DB");
            throw;
        }
    }

    public async Task<City> SendNewCityToDbAsync(string city, int countryId)
    {
        try
        {
            _logger.LogInformation("Sending new city to DB");

            ArgumentException.ThrowIfNullOrEmpty(city);

            var response = await _dbContext.Cities.AddAsync(new City()
            {
                Name = city,
                Country = await _dbContext.FindAsync<Country>(countryId) ??
                throw new InvalidOperationException($"Country with ID {countryId} does not exist")
            });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new city to DB");
            throw;
        }
    }
        
    public async Task<Make> SendNewMakeToDbAsync(string make)
    {
        try
        {
            _logger.LogInformation("Sending new make to DB");

            ArgumentException.ThrowIfNullOrEmpty(make);

            var response = await _dbContext.Makes.AddAsync(new Make() { Title = make });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new make to DB");
            throw;
        }
    }

    public async Task<Model> SendNewModelToDbAsync(int makeId, string model)
    {
        try
        {
            _logger.LogInformation("Sending new model to DB");

            ArgumentException.ThrowIfNullOrEmpty(model);

            var response = await _dbContext.Models.AddAsync(new Model() 
            { 
                Title = model, 
                Make = await _dbContext.Makes.FindAsync(makeId) ??
                throw new InvalidOperationException($"Make with ID {makeId} does not exist")
            });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new model to DB");
            throw;
        }
    }

    public async Task<FuelType> SendNewVehicleFuelTypeToDbAsync(string fuelType)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle fuel type to DB");

            ArgumentException.ThrowIfNullOrEmpty(fuelType);

            var response = await _dbContext.VehicleFuelTypes.AddAsync(new FuelType() { Type = fuelType });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle fuel type to DB");
            throw;
        }
    }

    public async Task<Condition> SendNewVehicleConditionToDbAsync(string condition, string description)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle condition to DB");

            ArgumentException.ThrowIfNullOrEmpty(condition);

            var response = await _dbContext.VehicleDetailsConditions.AddAsync(new Condition()
            {
                Title = condition, 
                Description = description
            });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle condition to DB");
            throw;
        }
    }

    public async Task<MarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle market version to DB");

            ArgumentException.ThrowIfNullOrEmpty(marketVersion);

            var response = await _dbContext.VehicleMarketVersions.AddAsync(new MarketVersion() { Version = marketVersion });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle market version to DB");
            throw;
        }
    }

    public async Task<Option> SendNewVehicleOptionToDbAsync(string option)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle option to DB");

            ArgumentException.ThrowIfNullOrEmpty(option);

            var response = await _dbContext.VehicleDetailsOptions.AddAsync(new Option() { Title = option });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle option to DB");
            throw;
        }
    }

    public async Task<Subscription> SendNewSubscriptionToDbAsync(string subscriptionName, decimal price)
    {
        try
        {
            _logger.LogInformation("Sending new subscription to DB");

            ArgumentException.ThrowIfNullOrEmpty(subscriptionName);

            var response = await _dbContext.Subscriptions.AddAsync(new Subscription()
            {
                Title = subscriptionName,
                Price = new PriceDetail()
                {
                    Price = price,
                }
            });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new subscription to DB");
            throw;
        }
    }

    public async Task<Color> UpdateVehicleColorInDbAsync(int colorId, string newColor)
    {
        try
        {
            _logger.LogInformation($"Updating color with ID {colorId} to new value {newColor} in DB");

            ArgumentException.ThrowIfNullOrEmpty(newColor);

            var color = await _dbContext.FindAsync<Color>(colorId) ?? new Color();
            color.Title = newColor;
            
            var response = _dbContext.Update(color);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return color;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating color with ID {colorId} to new value {newColor} in DB");
            throw;
        }
    }
        
    public async Task<BodyType> UpdateVehicleBodyTypeInDbAsync(int bodyTypeId, string newBodyType)
    {
        try
        {
            _logger.LogInformation($"Updating body type with ID {bodyTypeId} to new value {newBodyType} in DB");

            ArgumentException.ThrowIfNullOrEmpty(newBodyType);

            var bodyType = await _dbContext.FindAsync<BodyType>(bodyTypeId) ?? 
            throw new InvalidOperationException($"Body type with ID {bodyTypeId} does not exist");
            bodyType.Type = newBodyType;
            var response = _dbContext.Update(bodyType);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return bodyType;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating body type with ID {bodyTypeId} to new value {newBodyType} in DB");
            throw;
        }
    }

    public async Task<Country> UpdateCountryInDbAsync(int countryId, string newCountry)
    {
        try
        {
            _logger.LogInformation($"Updating country with ID {countryId} to new value {newCountry} in DB");

            ArgumentException.ThrowIfNullOrEmpty(newCountry);

            var country = await _dbContext.FindAsync<Country>(countryId) ??
            throw new InvalidOperationException($"Country with ID {countryId} does not exist");
            
            country.Name = newCountry;
            
            var response = _dbContext.Update(country);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return country;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating country with ID {countryId} to new value {newCountry} in DB");
            throw;
        }
    }
        
    public async Task<City> UpdateCityInDbAsync(int cityId, string newCity)
    {
        try
        {
            _logger.LogInformation($"Updating city with ID {cityId} to new value {newCity} in DB");

            ArgumentException.ThrowIfNullOrEmpty(newCity);

            var city = await _dbContext.FindAsync<City>(cityId) ??
            throw new InvalidOperationException($"City with ID {cityId} does not exist");

            city.Name = newCity;
            
            var response = _dbContext.Update(city);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return city;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating city with ID {cityId} to new value {newCity} in DB");
            throw;
        }
    }

    public async Task<Subscription> UpdateSubscriptionInDbAsync(int subscriptionId, decimal price)
    {
        try
        {
            _logger.LogInformation($"Updating subscription with ID {subscriptionId} in DB");
                
            var subscription = await _dbContext.FindAsync<Subscription>(subscriptionId) ??
            throw new InvalidOperationException($"Subscription with ID {subscriptionId} does not exist");

            subscription.Price.Price = price;
            
            var response = _dbContext.Update(subscription);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return subscription;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating subscription with ID {subscriptionId} in DB");
            throw;
        }
    }

    public async Task<DrivetrainType> UpdateVehicleDrivetrainTypeInDbAsync(int driveTrainId, string newDrivetrain)
    {
        try
        {
            _logger.LogInformation($"Updating drivetrain with ID {driveTrainId} to new value {newDrivetrain} in DB");

            ArgumentException.ThrowIfNullOrEmpty(newDrivetrain);

            var drivetrain = await _dbContext.FindAsync<DrivetrainType>(driveTrainId) ??
            throw new InvalidOperationException($"Drivetrain with ID {driveTrainId} does not exist");
            
            drivetrain.Type = newDrivetrain;
            var response = _dbContext.Update(drivetrain);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return drivetrain;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating drivetrain with ID {driveTrainId} to new value {newDrivetrain}");
            throw;
        }
    }
        
    public async Task<GearboxType> UpdateVehicleGearboxTypeInDbAsync(int gearboxId, string newGearbox)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newGearbox);

            var gearbox = await _dbContext.FindAsync<GearboxType>(gearboxId) ?? 
            throw new InvalidOperationException($"Gearbox with ID {gearboxId} does not exist");

            gearbox.Type = newGearbox;
            var response = _dbContext.Update(gearbox);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return gearbox;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating gearbox with ID {gearboxId} to new value {newGearbox}");
            throw;
        }
    }
        
    public async Task<Make> UpdateMakeInDbAsync(int makeId, string newMake)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newMake);

            var make = await _dbContext.FindAsync<Make>(makeId) ??
            throw new InvalidOperationException($"Make with ID {makeId} does not exist");

            make.Title = newMake;
            var response = _dbContext.Update(make);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return make;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating make with ID {makeId} to new value {newMake}");
            throw;
        }
    }
        
    public async Task<Model> UpdateModelInDbAsync(int modelId, string newModel)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newModel);

            var model = await _dbContext.FindAsync<Model>(modelId);

            if (model == null)
            {
                throw new ArgumentException();
            }
            
            model.Title = newModel;
            var response = _dbContext.Update(model);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return model;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating model with ID {modelId} to new value {newModel}");
            throw;
        }
    }

    public async Task<AccountLimit> UpdateAccountLimitInDbAsync(int limitId, int premiumLimit, int regularLimit)
    {
        try
        {
            if (premiumLimit < 0 || regularLimit < 0)
            {
                throw new ArgumentException();
            }

            var accountLimit = await _dbContext.FindAsync<AccountLimit>(limitId) ??
            throw new InvalidOperationException($"Limit with ID {limitId} does not exist");

            accountLimit.PremiumAnnouncementsLimit = premiumLimit;
            accountLimit.RegularAnnouncementsLimit = regularLimit;
            
            var response = _dbContext.Update(accountLimit);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return accountLimit;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating account limit with ID {limitId}");
            throw;
        }
    }
        
    public async Task<FuelType> UpdateFuelTypeInDbAsync(int fuelTypeId, string newFuelType)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newFuelType);

            var fuelType = await _dbContext.FindAsync<FuelType>(fuelTypeId) ??
            throw new InvalidOperationException($"Fuel type with ID {fuelTypeId} does not exist");

            fuelType.Type = newFuelType;
            var response = _dbContext.Update(fuelType);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return fuelType;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating fuel type with ID {fuelTypeId} to new value {newFuelType}");
            throw;
        }
    }
        
    public async Task<Condition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition, string newDescription)
    {
        try
        {
            if (string.IsNullOrEmpty(newVehicleCondition) || string.IsNullOrEmpty(newDescription))
            {
                throw new ArgumentException();
            }

            var vehicleCondition = await _dbContext.FindAsync<Condition>(vehicleConditionId) ??
            throw new InvalidOperationException($"Condition with ID {vehicleConditionId} does not exist");

            vehicleCondition.Title = newVehicleCondition;
            vehicleCondition.Description = newDescription;
            
            var response = _dbContext.Update(vehicleCondition);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleCondition;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating vehicle condition type with ID {vehicleConditionId} to new value {newVehicleCondition}");
            throw;
        }
    }
        
    public async Task<Option> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newVehicleOption);

            var vehicleOption = await _dbContext.FindAsync<Option>(vehicleOptionId) ??
            throw new InvalidOperationException($"Option with ID {vehicleOptionId} does not exist");

            vehicleOption.Title = newVehicleOption;
            var response = _dbContext.Update(vehicleOption);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleOption;   
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating vehicle option type with ID {vehicleOptionId} to new value {newVehicleOption}");
            throw;
        }
    }
        
    public async Task<MarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newMarketVersion);

            var marketVersion = await _dbContext.FindAsync<MarketVersion>(marketVersionId) ?? 
            throw new InvalidOperationException($"Market version with ID {marketVersionId} does not exist");

            marketVersion.Version = newMarketVersion;
            var response = _dbContext.Update(marketVersion);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return marketVersion;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating market version type with ID {marketVersionId} to new value {newMarketVersion}");
            throw;
        }
    }

    public async Task<Color?> DeleteVehicleColorFromDbAsync(int colorId)
    {
        try
        {
            var color = await _dbContext.FindAsync<Color>(colorId);
            
            if (color != null)
            {
                var response = _dbContext.Remove(color);

                if (response.State == EntityState.Deleted)
                {
                    await _dbContext.SaveChangesAsync();
                    return color;
                }
                
                throw new InvalidOperationException("Object wasn't deleted");
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<BodyType?> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId)
    {
        var bodyType = await _dbContext.FindAsync<BodyType>(bodyTypeId);
            
        if (bodyType != null)
        {
            var response = _dbContext.Remove(bodyType);
                
            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return bodyType;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Country?> DeleteCountryFromDbAsync(int countryId)
    {
        var country = await _dbContext.FindAsync<Country>(countryId);
            
        if (country != null)
        {
            var cities = await _dbContext.Cities
                .Where(x => x.Country.Id == country.Id)
                .ToListAsync();
                
            _dbContext.Cities.RemoveRange(cities);
            var response = _dbContext.Remove(country);
                
            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return country;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<DrivetrainType?> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId)
    {
        var drivetrain = await _dbContext.FindAsync<DrivetrainType>(driveTrainId);
            
        if (drivetrain != null)
        {
            var response = _dbContext.Remove(drivetrain);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return drivetrain;   
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<GearboxType?> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId)
    {
        var gearbox = await _dbContext.FindAsync<GearboxType>(gearboxId);
            
        if (gearbox != null)
        {
            var response = _dbContext.Remove(gearbox);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return gearbox;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Subscription?> DeleteSubscriptionFromDbAsync(int subscriptionId)
    {
        var subscription = await _dbContext.FindAsync<Subscription>(subscriptionId);
            
        if (subscription != null)
        {
            var response = _dbContext.Remove(subscription);
                
            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return subscription;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Make?> DeleteMakeFromDbAsync(int makeId)
    {
        var make = await _dbContext.FindAsync<Make>(makeId);
            
        if (make != null)
        {
            var response = _dbContext.Remove(make);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return make;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<City?> DeleteCityFromDbAsync(int cityId)
    {
        var city = await _dbContext.FindAsync<City>(cityId);
            
        if (city != null)
        {
            var response = _dbContext.Remove(city);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return city;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }
        
    public async Task<Model?> DeleteModelFromDbAsync(int modelId)
    {
        var model = await _dbContext.FindAsync<Model>(modelId);
            
        if (model != null)
        {
            var response = _dbContext.Remove(model);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return model;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<FuelType?> DeleteFuelTypeFromDbAsync(int fuelTypeId)
    {
        var fuelType = await _dbContext.FindAsync<FuelType>(fuelTypeId);
            
        if (fuelType != null)
        {
            var response = _dbContext.Remove(fuelType);
                
            if(response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return fuelType;   
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Condition?> DeleteVehicleConditionFromDbAsync(int vehicleConditionId)
    {
        var vehicleCondition = await _dbContext.FindAsync<Condition>(vehicleConditionId);
            
        if (vehicleCondition != null)
        {
            var response = _dbContext.Remove(vehicleCondition);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleCondition;   
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Option?> DeleteVehicleOptionFromDbAsync(int vehicleOptionId)
    {
        var vehicleOption = await _dbContext.FindAsync<Option>(vehicleOptionId);
            
        if (vehicleOption != null)
        {
            var response = _dbContext.Remove(vehicleOption);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleOption;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<MarketVersion?> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId)
    {
        var marketVersion = await _dbContext.FindAsync<MarketVersion>(marketVersionId);
            
        if (marketVersion != null)
        {
            var response = _dbContext.Remove(marketVersion);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return marketVersion;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<ApplicationUser?> DeleteModeratorFromDbAsync(Guid moderatorId)
    {
        var moderator = await _dbContext.Users
            .Where(x => x.Id == moderatorId)
            .FirstOrDefaultAsync();

        if (moderator == null)
        {
            return null;
        }
            
        var response = _dbContext.Remove(moderatorId);
            
        if (response.State == EntityState.Deleted)
        {
            await _dbContext.SaveChangesAsync();
            return moderator;
        }
            
        throw new InvalidOperationException("Object wasn't deleted");
    }

    public async Task<PaginatedList<ApplicationUser>> GetAllUsersFromDbAsync(PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogError($"Getting all users from db");

            var query = _dbContext.Users
                .Where(x => x.EmailConfirmed)
                .Join(_dbContext.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new
                    {
                        User = user, UserRole = userRole
                    })
                .Where(joined => !_dbContext.Roles
                    .Any(r => r.Id == joined.UserRole.RoleId &&
                              (r.Name == UserType.Admin.ToString() || r.Name == UserType.Moderator.ToString())))
                .Select(joined => joined.User)
                .Include(u => u.PhoneNumbers);
            
            var totalCount = await query.CountAsync();
            var users = await query
                    .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                    .Take(pagingParameters.PageSize)
                    .ToListAsync();
            
            if (users.IsNullOrEmpty())
            {
                return new PaginatedList<ApplicationUser>();
            }

            var paginatedUsers = PaginatedList<ApplicationUser>.Create(users, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);

            return paginatedUsers;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting all users from db");
            throw;
        }
    }

    public async Task<bool> BanUserInDbAsync(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        if (!user.IsBanned)
        {
            user.IsBanned = true;

            var result = _dbContext.Update(user);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }

        return false;
    }

    public async Task<bool> UnbanUserInDbAsync(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        if (user.IsBanned)
        {
            user.IsBanned = false;

            var result = _dbContext.Update(user);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }

        return false;
    }
}
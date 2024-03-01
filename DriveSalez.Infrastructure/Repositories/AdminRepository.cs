using AutoMapper;
using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Domain.Entities.VehicleParts;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Infrastructure.Repositories
{
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

        public async Task<VehicleColor> SendNewColorToDbAsync(string color)
        {
            try
            {
                _logger.LogInformation($"Sending new color to DB");

                if (string.IsNullOrEmpty(color))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleColors.AddAsync(new VehicleColor() { Color = color });

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
                
                if (string.IsNullOrEmpty(country))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.Countries.AddAsync(new Country() { CountryName = country });

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
        
        public async Task<VehicleBodyType> SendNewBodyTypeToDbAsync(string bodyType)
        {
            try
            {
                _logger.LogInformation($"Sending new body type to DB");
                
                if (string.IsNullOrEmpty(bodyType))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleBodyTypes.AddAsync(new VehicleBodyType() { BodyType = bodyType });

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

        public async Task<VehicleDrivetrainType> SendNewVehicleDrivetrainTypeToDbAsync(string driveTrainType)
        {
            try
            {
                _logger.LogInformation($"Sending new vehicle drivetrain type to DB");
                
                if (string.IsNullOrEmpty(driveTrainType))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleDriveTrainTypes.AddAsync(new VehicleDrivetrainType() { DrivetrainType = driveTrainType });
           
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

        public async Task<VehicleGearboxType> SendNewVehicleGearboxTypeToDbAsync(string gearboxType)
        {
            try
            {
                _logger.LogInformation("Sending new vehicle gearbox type to DB");
                
                if (string.IsNullOrEmpty(gearboxType))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleGearboxTypes.AddAsync(new VehicleGearboxType() { GearboxType = gearboxType });
            
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
                
                if (string.IsNullOrEmpty(city))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.Cities.AddAsync(new City()
                {
                    CityName = city,
                    Country = await _dbContext.FindAsync<Country>(countryId)
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
                
                if (string.IsNullOrEmpty(make))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.Makes.AddAsync(new Make() { MakeName = make });
            
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
                
                if (string.IsNullOrEmpty(model))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.Models.AddAsync(new Model() { ModelName = model, Make = await _dbContext.Makes.FindAsync(makeId) });

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

        public async Task<VehicleFuelType> SendNewVehicleFuelTypeToDbAsync(string fuelType)
        {
            try
            {
                _logger.LogInformation("Sending new vehicle fuel type to DB");
                
                if (string.IsNullOrEmpty(fuelType))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleFuelTypes.AddAsync(new VehicleFuelType() { FuelType = fuelType });
            
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

        public async Task<VehicleCondition> SendNewVehicleConditionToDbAsync(string condition, string description)
        {
            try
            {
                _logger.LogInformation("Sending new vehicle condition to DB");
                
                if (string.IsNullOrEmpty(condition))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleDetailsConditions.AddAsync(new VehicleCondition()
                {
                    Condition = condition, 
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

        public async Task<VehicleMarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion)
        {
            try
            {
                _logger.LogInformation("Sending new vehicle market version to DB");
                
                if (string.IsNullOrEmpty(marketVersion))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleMarketVersions.AddAsync(new VehicleMarketVersion() { MarketVersion = marketVersion });
            
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

        public async Task<VehicleOption> SendNewVehicleOptionToDbAsync(string option)
        {
            try
            {
                _logger.LogInformation("Sending new vehicle option to DB");
                
                if (string.IsNullOrEmpty(option))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.VehicleDetailsOptions.AddAsync(new VehicleOption() { Option = option });
            
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

        public async Task<Subscription> SendNewSubscriptionToDbAsync(string subscriptionName, decimal price, int currencyId)
        {
            try
            {
                _logger.LogInformation("Sending new subscription to DB");
                
                if (string.IsNullOrEmpty(subscriptionName))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.Subscriptions.AddAsync(new Subscription()
                {
                    SubscriptionName = subscriptionName,
                    Price = new SubscriptionPrice()
                    {
                        Price = price,
                        Currency = await _dbContext.FindAsync<Currency>(currencyId)
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

        public async Task<Currency> SendNewCurrencyToDbAsync(string currencyName)
        {
            try
            {
                _logger.LogInformation("Sending new currency to DB");
                
                if (string.IsNullOrEmpty(currencyName))
                {
                    throw new ArgumentException();
                }

                var response = await _dbContext.Currencies.AddAsync(new Currency() { CurrencyName = currencyName });
            
                if (response.State == EntityState.Added)
                {
                    await _dbContext.SaveChangesAsync();
                    return response.Entity;
                }

                throw new InvalidOperationException("Object wasn't added");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error sending new currency to DB");
                throw;
            }
        }

        public async Task<VehicleColor> UpdateVehicleColorInDbAsync(int colorId, string newColor)
        {
            try
            {
                _logger.LogInformation($"Updating color with ID {colorId} to new value {newColor} in DB");
                
                if (string.IsNullOrEmpty(newColor))
                {
                    throw new ArgumentException();
                }

                var color = await _dbContext.FindAsync<VehicleColor>(colorId) ?? new VehicleColor();
                color.Color = newColor;
            
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
        
        public async Task<VehicleBodyType> UpdateVehicleBodyTypeInDbAsync(int bodyTypeId, string newBodyType)
        {
            try
            {
                _logger.LogInformation($"Updating body type with ID {bodyTypeId} to new value {newBodyType} in DB");
                
                if (string.IsNullOrEmpty(newBodyType))
                {
                    throw new ArgumentException();
                }

                var bodyType = await _dbContext.FindAsync<VehicleBodyType>(bodyTypeId);
                bodyType.BodyType = newBodyType;
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
                
                if (string.IsNullOrEmpty(newCountry))
                {
                    throw new ArgumentException();
                }

                var country = await _dbContext.FindAsync<Country>(countryId);
                country.CountryName = newCountry;
            
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
                
                if (string.IsNullOrEmpty(newCity))
                {
                    throw new ArgumentException();
                }

                var city = await _dbContext.FindAsync<City>(cityId);
                city.CityName = newCity;
            
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
        
        public async Task<Currency> UpdateCurrencyInDbAsync(int currencyId, string currencyName)
        {
            try
            {
                _logger.LogInformation($"Updating currency with ID {currencyId} to new value {currencyName}");
                
                if (string.IsNullOrEmpty(currencyName))
                {
                    throw new ArgumentException();
                }

                var currency = await _dbContext.FindAsync<Currency>(currencyId);
                currency.CurrencyName = currencyName;
                var response = _dbContext.Update(currency);

                if (response.State == EntityState.Modified)
                {
                    await _dbContext.SaveChangesAsync();
                    return currency;
                }

                throw new InvalidOperationException("Object wasn't modified");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error updating currency with ID {currencyId} to new value {currencyName}");
                throw;
            }
        }

        public async Task<Subscription> UpdateSubscriptionInDbAsync(int subscriptionId, decimal price, int currencyId)
        {
            try
            {
                _logger.LogInformation($"Updating subscription with ID {subscriptionId} in DB");
                
                var subscription = await _dbContext.FindAsync<Subscription>(subscriptionId);

                subscription.Price.Price = price;
                subscription.Price.Currency = await _dbContext.Currencies.FindAsync(currencyId);
            
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

        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDbAsync(int driveTrainId, string newDrivetrain)
        {
            try
            {
                _logger.LogInformation($"Updating drivetrain with ID {driveTrainId} to new value {newDrivetrain} in DB");
                
                if (driveTrainId == null || string.IsNullOrEmpty(newDrivetrain))
                {
                    throw new ArgumentException();
                }

                var drivetrain = await _dbContext.FindAsync<VehicleDrivetrainType>(driveTrainId);
                drivetrain.DrivetrainType = newDrivetrain;
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
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDbAsync(int gearboxId, string newGearbox)
        {
            try
            {
                if (gearboxId == null || string.IsNullOrEmpty(newGearbox))
                {
                    throw new ArgumentException();
                }

                var gearbox = await _dbContext.FindAsync<VehicleGearboxType>(gearboxId);
                gearbox.GearboxType = newGearbox;
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
                if (makeId == null || string.IsNullOrEmpty(newMake))
                {
                    throw new ArgumentException();
                }

                var make = await _dbContext.FindAsync<Make>(makeId);
                make.MakeName = newMake;
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
                if (modelId == null || string.IsNullOrEmpty(newModel))
                {
                    throw new ArgumentException();
                }

                var model = await _dbContext.FindAsync<Model>(modelId);

                if (model == null)
                {
                    throw new ArgumentException();
                }
            
                model.ModelName = newModel;
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
                if (limitId == null || premiumLimit < 0 || regularLimit < 0)
                {
                    throw new ArgumentException();
                }

                var accountLimit = await _dbContext.FindAsync<AccountLimit>(limitId);
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
        
        public async Task<VehicleFuelType> UpdateFuelTypeInDbAsync(int fuelTypeId, string newFuelType)
        {
            try
            {
                if (fuelTypeId == null || string.IsNullOrEmpty(newFuelType))
                {
                    throw new ArgumentException();
                }

                var fuelType = await _dbContext.FindAsync<VehicleFuelType>(fuelTypeId);
                fuelType.FuelType = newFuelType;
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
        
        public async Task<VehicleCondition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition, string newDescription)
        {
            try
            {
                if (vehicleConditionId == null || string.IsNullOrEmpty(newVehicleCondition) || string.IsNullOrEmpty(newDescription))
                {
                    throw new ArgumentException();
                }

                var vehicleCondition = await _dbContext.FindAsync<VehicleCondition>(vehicleConditionId);
                vehicleCondition.Condition = newVehicleCondition;
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
        
        public async Task<VehicleOption> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption)
        {
            try
            {
                if (vehicleOptionId == null || string.IsNullOrEmpty(newVehicleOption))
                {
                    throw new ArgumentException();
                }

                var vehicleOption = await _dbContext.FindAsync<VehicleOption>(vehicleOptionId);
                vehicleOption.Option = newVehicleOption;
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
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion)
        {
            try
            {
                if (marketVersionId == null || string.IsNullOrEmpty(newMarketVersion))
                {
                    throw new ArgumentException();
                }

                var marketVersion = await _dbContext.FindAsync<VehicleMarketVersion>(marketVersionId);
                marketVersion.MarketVersion = newMarketVersion;
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

        public async Task<VehicleColor?> DeleteVehicleColorFromDbAsync(int colorId)
        {
            try
            {
                if (colorId == null)
                {
                    throw new ArgumentException();
                }

                var color = await _dbContext.FindAsync<VehicleColor>(colorId);
            
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

        public async Task<VehicleBodyType?> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId)
        {
            if (bodyTypeId == null)
            {
                throw new ArgumentException();
            }

            var bodyType = await _dbContext.FindAsync<VehicleBodyType>(bodyTypeId);
            
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
            if (countryId == null)
            {
                throw new ArgumentException();
            }

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
        
        public async Task<Currency?> DeleteCurrencyFromDbAsync(int currencyId)
        {
            if (currencyId == null)
            {
                throw new ArgumentException();
            }

            var currency = await _dbContext.FindAsync<Currency>(currencyId);
            
            if (currency != null)
            {
                var response = _dbContext.Remove(currency);
                
                if (response.State == EntityState.Deleted)
                {
                    await _dbContext.SaveChangesAsync();
                    return currency;
                }
                
                throw new InvalidOperationException("Object wasn't deleted");
            }
            
            return null;
        }

        public async Task<VehicleDrivetrainType?> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId)
        {
            if (driveTrainId == null)
            {
                throw new ArgumentException();
            }

            var drivetrain = await _dbContext.FindAsync<VehicleDrivetrainType>(driveTrainId);
            
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

        public async Task<VehicleGearboxType?> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId)
        {
            if (gearboxId == null)
            {
                throw new ArgumentException();
            }

            var gearbox = await _dbContext.FindAsync<VehicleGearboxType>(gearboxId);
            
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
            if (subscriptionId == null)
            {
                throw new ArgumentException();
            }

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
            if (makeId == null)
            {
                throw new ArgumentException();
            }

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
            if (cityId == null)
            {
                throw new ArgumentException();
            }

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
            if (modelId == null)
            {
                throw new ArgumentException();
            }

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

        public async Task<VehicleFuelType?> DeleteFuelTypeFromDbAsync(int fuelTypeId)
        {
            if (fuelTypeId == null)
            {
                throw new ArgumentException();
            }

            var fuelType = await _dbContext.FindAsync<VehicleFuelType>(fuelTypeId);
            
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

        public async Task<VehicleCondition?> DeleteVehicleConditionFromDbAsync(int vehicleConditionId)
        {
            if (vehicleConditionId == null)
            {
                throw new ArgumentException();
            }

            var vehicleCondition = await _dbContext.FindAsync<VehicleCondition>(vehicleConditionId);
            
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

        public async Task<VehicleOption?> DeleteVehicleOptionFromDbAsync(int vehicleOptionId)
        {
            if (vehicleOptionId == null)
            {
                throw new ArgumentException();
            }

            var vehicleOption = await _dbContext.FindAsync<VehicleOption>(vehicleOptionId);
            
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

        public async Task<VehicleMarketVersion?> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId)
        {
            if (marketVersionId == null)
            {
                throw new ArgumentException();
            }

            var marketVersion = await _dbContext.FindAsync<VehicleMarketVersion>(marketVersionId);
            
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

        public async Task<GetModeratorDto?> DeleteModeratorFromDbAsync(Guid moderatorId)
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
                return new GetModeratorDto()
                {
                    Id = moderator.Id,
                    Name = moderator.FirstName,
                    Surname = moderator.LastName,
                    Email = moderator.UserName
                };
            }
            
            throw new InvalidOperationException("Object wasn't deleted");
        }

        public async Task<IEnumerable<GetUserDto>> GetAllUsersFromDbAsync()
        {
            try
            {
                _logger.LogError($"Getting all users from db");

                var users = await _dbContext.Users
                    .Join(_dbContext.UserRoles,
                        user => user.Id,
                        userRole => userRole.UserId,
                        (user, userRole) => new
                        {
                            User = user, UserRole = userRole
                        })
                    .Where(joined => !_dbContext.Roles
                        .Any(r => r.Id == joined.UserRole.RoleId && (r.Name == UserType.Admin.ToString() || r.Name == UserType.Moderator.ToString())))
                    .Select(joined => joined.User)
                    .Include(u => u.PhoneNumbers)
                    .ToListAsync();
                
                if (users.IsNullOrEmpty())
                {
                    return Enumerable.Empty<GetUserDto>();
                }

                return _mapper.Map<IEnumerable<GetUserDto>>(users);
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
}

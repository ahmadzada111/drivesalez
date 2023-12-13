using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VehicleColor> SendNewColorToDbAsync(string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return null;
            }

            var response = await _dbContext.VehicleColors.AddAsync(new VehicleColor() { Color = color });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleBodyType> SendNewBodyTypeToDbAsync(string bodyType)
        {
            if (string.IsNullOrEmpty(bodyType))
            {
                return null;
            }

            var response = await _dbContext.VehicleBodyTypes.AddAsync(new VehicleBodyType() { BodyType = bodyType });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleDrivetrainType> SendNewVehicleDrivetrainTypeToDbAsync(string driveTrainType)
        {
            if (string.IsNullOrEmpty(driveTrainType))
            {
                return null;
            }

            var response = await _dbContext.VehicleDriveTrainTypes.AddAsync(new VehicleDrivetrainType() { DrivetrainType = driveTrainType });
           
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleGearboxType> SendNewVehicleGearboxTypeToDbAsync(string gearboxType)
        {
            if (string.IsNullOrEmpty(gearboxType))
            {
                return null;
            }

            var response = await _dbContext.VehicleGearboxTypes.AddAsync(new VehicleGearboxType() { GearboxType = gearboxType });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<Make> SendNewMakeToDbAsync(string make)
        {
            if (string.IsNullOrEmpty(make))
            {
                return null;
            }

            var response = await _dbContext.Makes.AddAsync(new Make() { MakeName = make });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<Model> SendNewModelToDbAsync(int makeId, string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return null;
            }

            var response = await _dbContext.Models.AddAsync(new Core.Entities.Model() { ModelName = model, Make = await _dbContext.Makes.FindAsync(makeId) });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleFuelType> SendNewVehicleFuelTypeToDbAsync(string fuelType)
        {
            if (string.IsNullOrEmpty(fuelType))
            {
                return null;
            }

            var response = await _dbContext.VehicleFuelTypes.AddAsync(new VehicleFuelType() { FuelType = fuelType });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleCondition> SendNewVehicleDetailsConditionToDbAsync(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return null;
            }

            var response = await _dbContext.VehicleDetailsConditions.AddAsync(new VehicleCondition() { Condition = condition });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleMarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion)
        {
            if (string.IsNullOrEmpty(marketVersion))
            {
                return null;
            }

            var response = await _dbContext.VehicleMarketVersions.AddAsync(new VehicleMarketVersion() { MarketVersion = marketVersion });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleOption> SendNewVehicleDetailsOptionsToDbAsync(string option)
        {
            if (string.IsNullOrEmpty(option))
            {
                return null;
            }

            var response = await _dbContext.VehicleDetailsOptions.AddAsync(new VehicleOption() { Option = option });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<Subscription> SendNewSubscriptionToDbAsync(string subscriptionName, decimal price, int currencyId)
        {
            if (string.IsNullOrEmpty(subscriptionName))
            {
                return null;
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

            return null;
        }

        public async Task<Currency> SendNewCurrencyToDbAsync(string currencyName)
        {
            if (string.IsNullOrEmpty(currencyName))
            {
                return null;
            }

            var response = await _dbContext.Currencies.AddAsync(new Currency() { CurrencyName = currencyName });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            return null;
        }

        public async Task<VehicleColor> UpdateVehicleColorInDbAsync(int colorId, string newColor)
        {
            if (string.IsNullOrEmpty(newColor))
            {
                return null;
            }

            var color = await _dbContext.FindAsync<VehicleColor>(colorId);
            color.Color = newColor;
            
            var response = _dbContext.Update(color);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return color;
            }

            return null;
        }
        
        public async Task<VehicleBodyType> UpdateVehicleBodyTypeInDbAsync(int bodyTypeId, string newBodyType)
        {
            if (string.IsNullOrEmpty(newBodyType))
            {
                return null;
            }

            var bodyType = await _dbContext.FindAsync<VehicleBodyType>(bodyTypeId);
            bodyType.BodyType = newBodyType;
            var response = _dbContext.Update(bodyType);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return bodyType;
            }

            return null;
        }

        public async Task<Currency> UpdateCurrencyInDbAsync(int currencyId, string currencyName)
        {
            if (string.IsNullOrEmpty(currencyName))
            {
                return null;
            }

            var currency = await _dbContext.FindAsync<Currency>(currencyId);
            currency.CurrencyName = currencyName;
            var response = _dbContext.Update(currency);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return currency;
            }

            return null;
        }

        public async Task<Subscription> UpdateSubscriptionInDbAsync(int subscriptionId, string subscriptionName, decimal price, int currencyId)
        {
            if (string.IsNullOrEmpty(subscriptionName))
            {
                return null;
            }

            var subscription = await _dbContext.FindAsync<Subscription>(subscriptionId);
            subscription.SubscriptionName = subscriptionName;
            subscription.Price.Price = price;
            subscription.Price.Currency = await _dbContext.Currencies.FindAsync(currencyId);
            
            var response = _dbContext.Update(subscription);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return subscription;
            }

            return null;
        }

        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDbAsync(int driveTrainId, string newDrivetrain)
        {
            if (driveTrainId == null || string.IsNullOrEmpty(newDrivetrain))
            {
                return null;
            }

            var drivetrain = await _dbContext.FindAsync<VehicleDrivetrainType>(driveTrainId);
            drivetrain.DrivetrainType = newDrivetrain;
            var response = _dbContext.Update(drivetrain);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return drivetrain;
            }

            return null;
        }
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDbAsync(int gearboxId, string newGearbox)
        {
            if (gearboxId == null || string.IsNullOrEmpty(newGearbox))
            {
                return null;
            }

            var gearbox = await _dbContext.FindAsync<VehicleGearboxType>(gearboxId);
            gearbox.GearboxType = newGearbox;
            var response = _dbContext.Update(gearbox);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return gearbox;
            }

            return null;
        }
        
        public async Task<Make> UpdateMakeInDbAsync(int makeId, string newMake)
        {
            if (makeId == null || string.IsNullOrEmpty(newMake))
            {
                return null;
            }

            var make = await _dbContext.FindAsync<Make>(makeId);
            make.MakeName = newMake;
            var response = _dbContext.Update(make);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return make;
            }

            return null;
        }
        
        public async Task<Model> UpdateModelInDbAsync(int modelId, string newModel)
        {
            if (modelId == null || string.IsNullOrEmpty(newModel))
            {
                return null;
            }

            var model = await _dbContext.FindAsync<Model>(modelId);
            model.ModelName = newModel;
            var response = _dbContext.Update(model);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return model;
            }

            return null;
        }

        public async Task<AccountLimit> UpdateAccountLimitInDbAsync(int limitId, int limit)
        {
            if (limitId == null || limit == null)
            {
                return null;
            }

            var accountLimit = await _dbContext.FindAsync<AccountLimit>(limitId);
            accountLimit.PremiumAnnouncementsLimit = limit;
            var response = _dbContext.Update(accountLimit);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return accountLimit;
            }

            return null;
        }
        
        public async Task<VehicleFuelType> UpdateFuelTypeInDbAsync(int fuelTypeId, string newFuelType)
        {
            if (fuelTypeId == null || string.IsNullOrEmpty(newFuelType))
            {
                return null;
            }

            var fuelType = await _dbContext.FindAsync<VehicleFuelType>(fuelTypeId);
            fuelType.FuelType = newFuelType;
            var response = _dbContext.Update(fuelType);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return fuelType;
            }

            return null;
        }
        
        public async Task<VehicleCondition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition)
        {
            if (vehicleConditionId == null || string.IsNullOrEmpty(newVehicleCondition))
            {
                return null;
            }

            var vehicleCondition = await _dbContext.FindAsync<VehicleCondition>(vehicleConditionId);
            vehicleCondition.Condition = newVehicleCondition;
            var response = _dbContext.Update(vehicleCondition);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleCondition;
            }

            return null;
        }
        
        public async Task<VehicleOption> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption)
        {
            if (vehicleOptionId == null || string.IsNullOrEmpty(newVehicleOption))
            {
                return null;
            }

            var vehicleOption = await _dbContext.FindAsync<VehicleOption>(vehicleOptionId);
            vehicleOption.Option = newVehicleOption;
            var response = _dbContext.Update(vehicleOption);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleOption;   
            }

            return null;
        }
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion)
        {
            if (marketVersionId == null || string.IsNullOrEmpty(newMarketVersion))
            {
                return null;
            }

            var marketVersion = await _dbContext.FindAsync<VehicleMarketVersion>(marketVersionId);
            marketVersion.MarketVersion = newMarketVersion;
            var response = _dbContext.Update(marketVersion);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return marketVersion;
            }

            return null;
        }

        public async Task<VehicleColor> DeleteVehicleColorFromDbAsync(int colorId)
        {
            if (colorId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleBodyType> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId)
        {
            if (bodyTypeId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<Currency> DeleteCurrencyFromDbAsync(int currencyId)
        {
            if (currencyId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleDrivetrainType> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId)
        {
            if (driveTrainId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleGearboxType> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId)
        {
            if (gearboxId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<Subscription> DeleteSubscriptionFromDbAsync(int subscriptionId)
        {
            if (subscriptionId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<Make> DeleteMakeFromDbAsync(int makeId)
        {
            if (makeId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<Model> DeleteModelFromDbAsync(int modelId)
        {
            if (modelId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleFuelType> DeleteFuelTypeFromDbAsync(int fuelTypeId)
        {
            if (fuelTypeId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleCondition> DeleteVehicleConditionFromDbAsync(int vehicleConditionId)
        {
            if (vehicleConditionId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleOption> DeleteVehicleOptionFromDbAsync(int vehicleOptionId)
        {
            if (vehicleOptionId == null)
            {
                return null;
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
            }
            
            return null;
        }

        public async Task<VehicleMarketVersion> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId)
        {
            if (marketVersionId == null)
            {
                return null;
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
            }
            
            return null;
        }
    }
}

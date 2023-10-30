using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;

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
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<VehicleBodyType> SendNewBodyTypeToDbAsync(string bodyType)
        {
            if (string.IsNullOrEmpty(bodyType))
            {
                return null;
            }

            var responce = await _dbContext.VehicleBodyTypes.AddAsync(new VehicleBodyType() { BodyType = bodyType });
            await _dbContext.SaveChangesAsync();
            return responce.Entity;
        }

        public async Task<VehicleDrivetrainType> SendNewVehicleDrivetrainTypeToDbAsync(string driveTrainType)
        {
            if (string.IsNullOrEmpty(driveTrainType))
            {
                return null;
            }

            var response = await _dbContext.VehicleDriveTrainTypes.AddAsync(new VehicleDrivetrainType() { DrivetrainType = driveTrainType });
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<VehicleGearboxType> SendNewVehicleGearboxTypeToDbAsync(string gearboxType)
        {
            if (string.IsNullOrEmpty(gearboxType))
            {
                return null;
            }

            var response = await _dbContext.VehicleGearboxTypes.AddAsync(new VehicleGearboxType() { GearboxType = gearboxType });
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<Make> SendNewMakeToDbAsync(string make)
        {
            if (string.IsNullOrEmpty(make))
            {
                return null;
            }

            var response = await _dbContext.Makes.AddAsync(new Make() { MakeName = make });
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<Model> SendNewModelToDbAsync(int makeId, string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return null;
            }

            var response = await _dbContext.Models.AddAsync(new Core.Entities.Model() { ModelName = model, Make = await _dbContext.Makes.FindAsync(makeId) });

            if (response != null)
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
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<VehicleCondition> SendNewVehicleDetailsConditionToDbAsync(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return null;
            }

            var response = await _dbContext.VehicleDetailsConditions.AddAsync(new VehicleCondition() { Condition = condition });
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<VehicleMarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion)
        {
            if (string.IsNullOrEmpty(marketVersion))
            {
                return null;
            }

            var response = await _dbContext.VehicleMarketVersions.AddAsync(new VehicleMarketVersion() { MarketVersion = marketVersion });
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<VehicleOption> SendNewVehicleDetailsOptionsToDbAsync(string option)
        {
            if (string.IsNullOrEmpty(option))
            {
                return null;
            }

            var response = await _dbContext.VehicleDetailsOptions.AddAsync(new VehicleOption() { Option = option });
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<VehicleColor> UpdateVehicleColorInDbAsync(int colorId, string newColor)
        {
            if (colorId == null || string.IsNullOrEmpty(newColor))
            {
                return null;
            }

            var color = await _dbContext.FindAsync<VehicleColor>(colorId);
            color.Color = newColor;
            _dbContext.Update(color);
            await _dbContext.SaveChangesAsync();

            return color;
        }
        
        public async Task<VehicleBodyType> UpdateVehicleBodyTypeInDbAsync(int bodyTypeId, string newBodyType)
        {
            if (bodyTypeId == null || string.IsNullOrEmpty(newBodyType))
            {
                return null;
            }

            var bodyType = await _dbContext.FindAsync<VehicleBodyType>(bodyTypeId);
            bodyType.BodyType = newBodyType;
            _dbContext.Update(bodyType);
            await _dbContext.SaveChangesAsync();

            return bodyType;
        }
        
        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDbAsync(int driveTrainId, string newDrivetrain)
        {
            if (driveTrainId == null || string.IsNullOrEmpty(newDrivetrain))
            {
                return null;
            }

            var drivetrain = await _dbContext.FindAsync<VehicleDrivetrainType>(driveTrainId);
            drivetrain.DrivetrainType = newDrivetrain;
            _dbContext.Update(drivetrain);
            await _dbContext.SaveChangesAsync();

            return drivetrain;
        }
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDbAsync(int gearboxId, string newGearbox)
        {
            if (gearboxId == null || string.IsNullOrEmpty(newGearbox))
            {
                return null;
            }

            var gearbox = await _dbContext.FindAsync<VehicleGearboxType>(gearboxId);
            gearbox.GearboxType = newGearbox;
            _dbContext.Update(gearbox);
            await _dbContext.SaveChangesAsync();

            return gearbox;
        }
        
        public async Task<Make> UpdateMakeInDbAsync(int makeId, string newMake)
        {
            if (makeId == null || string.IsNullOrEmpty(newMake))
            {
                return null;
            }

            var make = await _dbContext.FindAsync<Make>(makeId);
            make.MakeName = newMake;
            _dbContext.Update(make);
            await _dbContext.SaveChangesAsync();

            return make;
        }
        
        public async Task<Model> UpdateModelInDbAsync(int modelId, string newModel)
        {
            if (modelId == null || string.IsNullOrEmpty(newModel))
            {
                return null;
            }

            var model = await _dbContext.FindAsync<Model>(modelId);
            model.ModelName = newModel;
            _dbContext.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
        
        public async Task<VehicleFuelType> UpdateFuelTypeInDbAsync(int fuelTypeId, string newFuelType)
        {
            if (fuelTypeId == null || string.IsNullOrEmpty(newFuelType))
            {
                return null;
            }

            var fuelType = await _dbContext.FindAsync<VehicleFuelType>(fuelTypeId);
            fuelType.FuelType = newFuelType;
            _dbContext.Update(fuelType);
            await _dbContext.SaveChangesAsync();

            return fuelType;
        }
        
        public async Task<VehicleCondition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition)
        {
            if (vehicleConditionId == null || string.IsNullOrEmpty(newVehicleCondition))
            {
                return null;
            }

            var vehicleCondition = await _dbContext.FindAsync<VehicleCondition>(vehicleConditionId);
            vehicleCondition.Condition = newVehicleCondition;
            _dbContext.Update(vehicleCondition);
            await _dbContext.SaveChangesAsync();

            return vehicleCondition;
        }
        
        public async Task<VehicleOption> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption)
        {
            if (vehicleOptionId == null || string.IsNullOrEmpty(newVehicleOption))
            {
                return null;
            }

            var vehicleOption = await _dbContext.FindAsync<VehicleOption>(vehicleOptionId);
            vehicleOption.Option = newVehicleOption;
            _dbContext.Update(vehicleOption);
            await _dbContext.SaveChangesAsync();

            return vehicleOption;
        }
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion)
        {
            if (marketVersionId == null || string.IsNullOrEmpty(newMarketVersion))
            {
                return null;
            }

            var marketVersion = await _dbContext.FindAsync<VehicleMarketVersion>(marketVersionId);
            marketVersion.MarketVersion = newMarketVersion;
            _dbContext.Update(marketVersion);
            await _dbContext.SaveChangesAsync();

            return marketVersion;
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
                _dbContext.Remove(color);
                await _dbContext.SaveChangesAsync();
                return color;
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
                _dbContext.Remove(bodyType);
                await _dbContext.SaveChangesAsync();
                return bodyType;
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
                _dbContext.Remove(drivetrain);
                await _dbContext.SaveChangesAsync();
                return drivetrain;
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
                _dbContext.Remove(gearbox);
                await _dbContext.SaveChangesAsync();
                return gearbox;
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
                _dbContext.Remove(make);
                await _dbContext.SaveChangesAsync();
                return make;
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
                _dbContext.Remove(model);
                await _dbContext.SaveChangesAsync();
                return model;
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
                _dbContext.Remove(fuelType);
                await _dbContext.SaveChangesAsync();
                return fuelType;
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
                _dbContext.Remove(vehicleCondition);
                await _dbContext.SaveChangesAsync();
                return vehicleCondition;
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
                _dbContext.Remove(vehicleOption);
                await _dbContext.SaveChangesAsync();
                return vehicleOption;
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
                _dbContext.Remove(marketVersion);
                await _dbContext.SaveChangesAsync();
                return marketVersion;
            }
            
            return null;
        }
    }
}

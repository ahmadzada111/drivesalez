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

        public VehicleColor SendNewColorToDb(string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return null;
            }

            var response = _dbContext.VehicleColors.Add(new VehicleColor() { Name = color }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleBodyType SendNewBodyTypeToDb(string bodyType)
        {
            if (string.IsNullOrEmpty(bodyType))
            {
                return null;
            }

            var responce = _dbContext.VehicleBodyTypes.Add(new VehicleBodyType() { Name = bodyType }).Entity;
            _dbContext.SaveChanges();
            return responce;
        }

        public VehicleDrivetrainType SendNewVehicleDrivetrainTypeToDb(string driveTrainType)
        {
            if (string.IsNullOrEmpty(driveTrainType))
            {
                return null;
            }

            var response = _dbContext.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { Name = driveTrainType }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleGearboxType SendNewVehicleGearboxTypeToDb(string gearboxType)
        {
            if (string.IsNullOrEmpty(gearboxType))
            {
                return null;
            }

            var response = _dbContext.VehicleGearboxTypes.Add(new VehicleGearboxType() { Name = gearboxType }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public Make SendNewMakeToDb(string make)
        {
            if (string.IsNullOrEmpty(make))
            {
                return null;
            }

            var response = _dbContext.Makes.Add(new Make() { Name = make }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public Model SendNewModelToDb(int makeId, string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return null;
            }

            var response = _dbContext.Models.Add(new Core.Entities.Model() { Name = model, Make = this._dbContext.Makes.Find(makeId) }).Entity;

            if (response != null)
            {
                this._dbContext.SaveChanges();
                return response;
            }

            return null;
        }

        public VehicleFuelType SendNewVehicleFuelTypeToDb(string fuelType)
        {
            if (string.IsNullOrEmpty(fuelType))
            {
                return null;
            }

            var response = _dbContext.VehicleFuelTypes.Add(new VehicleFuelType() { FuelType = fuelType }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleCondition SendNewVehicleDetailsConditionToDb(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return null;
            }

            var response = _dbContext.VehicleDetailsConditions.Add(new VehicleCondition() { Name = condition }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleMarketVersion SendNewVehicleMarketVersionToDb(string marketVersion)
        {
            if (string.IsNullOrEmpty(marketVersion))
            {
                return null;
            }

            var response = _dbContext.VehicleMarketVersions.Add(new VehicleMarketVersion() { Name = marketVersion }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleOption SendNewVehicleDetailsOptionsToDb(string option)
        {
            if (string.IsNullOrEmpty(option))
            {
                return null;
            }

            var response = _dbContext.VehicleDetailsOptions.Add(new VehicleOption() { Option = option }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public async Task<VehicleColor> UpdateVehicleColorInDb(int colorId, string newColor)
        {
            if (colorId == null || string.IsNullOrEmpty(newColor))
            {
                return null;
            }

            var color = await _dbContext.FindAsync<VehicleColor>(colorId);
            color.Name = newColor;
            _dbContext.Update(color);
            await _dbContext.SaveChangesAsync();

            return color;
        }
        
        public async Task<VehicleBodyType> UpdateVehicleBodyTypeInDb(int bodyTypeId, string newBodyType)
        {
            if (bodyTypeId == null || string.IsNullOrEmpty(newBodyType))
            {
                return null;
            }

            var bodyType = await _dbContext.FindAsync<VehicleBodyType>(bodyTypeId);
            bodyType.Name = newBodyType;
            _dbContext.Update(bodyType);
            await _dbContext.SaveChangesAsync();

            return bodyType;
        }
        
        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDb(int driveTrainId, string newDrivetrain)
        {
            if (driveTrainId == null || string.IsNullOrEmpty(newDrivetrain))
            {
                return null;
            }

            var drivetrain = await _dbContext.FindAsync<VehicleDrivetrainType>(driveTrainId);
            drivetrain.Name = newDrivetrain;
            _dbContext.Update(drivetrain);
            await _dbContext.SaveChangesAsync();

            return drivetrain;
        }
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDb(int gearboxId, string newGearbox)
        {
            if (gearboxId == null || string.IsNullOrEmpty(newGearbox))
            {
                return null;
            }

            var gearbox = await _dbContext.FindAsync<VehicleGearboxType>(gearboxId);
            gearbox.Name = newGearbox;
            _dbContext.Update(gearbox);
            await _dbContext.SaveChangesAsync();

            return gearbox;
        }
        
        public async Task<Make> UpdateMakeInDb(int makeId, string newMake)
        {
            if (makeId == null || string.IsNullOrEmpty(newMake))
            {
                return null;
            }

            var make = await _dbContext.FindAsync<Make>(makeId);
            make.Name = newMake;
            _dbContext.Update(make);
            await _dbContext.SaveChangesAsync();

            return make;
        }
        
        public async Task<Model> UpdateModelInDb(int modelId, string newModel)
        {
            if (modelId == null || string.IsNullOrEmpty(newModel))
            {
                return null;
            }

            var model = await _dbContext.FindAsync<Model>(modelId);
            model.Name = newModel;
            _dbContext.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
        
        public async Task<VehicleFuelType> UpdateFuelTypeInDb(int fuelTypeId, string newFuelType)
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
        
        public async Task<VehicleCondition> UpdateVehicleConditionInDb(int vehicleConditionId, string newVehicleCondition)
        {
            if (vehicleConditionId == null || string.IsNullOrEmpty(newVehicleCondition))
            {
                return null;
            }

            var vehicleCondition = await _dbContext.FindAsync<VehicleCondition>(vehicleConditionId);
            vehicleCondition.Name = newVehicleCondition;
            _dbContext.Update(vehicleCondition);
            await _dbContext.SaveChangesAsync();

            return vehicleCondition;
        }
        
        public async Task<VehicleOption> UpdateVehicleOptionInDb(int vehicleOptionId, string newVehicleOption)
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
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDb(int marketVersionId, string newMarketVersion)
        {
            if (marketVersionId == null || string.IsNullOrEmpty(newMarketVersion))
            {
                return null;
            }

            var marketVersion = await _dbContext.FindAsync<VehicleMarketVersion>(marketVersionId);
            marketVersion.Name = newMarketVersion;
            _dbContext.Update(marketVersion);
            await _dbContext.SaveChangesAsync();

            return marketVersion;
        }

        public async Task<VehicleColor> DeleteVehicleColorFromDb(int colorId)
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

        public async Task<VehicleBodyType> DeleteVehicleBodyTypeFromDb(int bodyTypeId)
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

        public async Task<VehicleDrivetrainType> DeleteVehicleDrivetrainTypeFromDb(int driveTrainId)
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

        public async Task<VehicleGearboxType> DeleteVehicleGearboxTypeFromDb(int gearboxId)
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

        public async Task<Make> DeleteMakeFromDb(int makeId)
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

        public async Task<Model> DeleteModelFromDb(int modelId)
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

        public async Task<VehicleFuelType> DeleteFuelTypeFromDb(int fuelTypeId)
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

        public async Task<VehicleCondition> DeleteVehicleConditionFromDb(int vehicleConditionId)
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

        public async Task<VehicleOption> DeleteVehicleOptionFromDb(int vehicleOptionId)
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

        public async Task<VehicleMarketVersion> DeleteVehicleMarketVersionFromDb(int marketVersionId)
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

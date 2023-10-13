using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.RepositoryContracts
{
    public interface IAdminRepository
    {
        VehicleColor SendNewColorToDb(string color);

        VehicleBodyType SendNewBodyTypeToDb(string bodyType);

        VehicleDrivetrainType SendNewVehicleDrivetrainTypeToDb(string driveTrainType);

        VehicleGearboxType SendNewVehicleGearboxTypeToDb(string gearboxType);

        Make SendNewMakeToDb(string make);

        Model SendNewModelToDb(int makeId, string model);

        VehicleFuelType SendNewVehicleFuelTypeToDb(string fuelType);

        VehicleCondition SendNewVehicleDetailsConditionToDb(string condition);

        VehicleMarketVersion SendNewVehicleMarketVersionToDb(string marketVersion);

        VehicleOption SendNewVehicleDetailsOptionsToDb(string option);

        Task<VehicleColor> UpdateVehicleColorInDb(int colorId, string newColor);

        Task<VehicleBodyType> UpdateVehicleBodyTypeInDb(int bodyTypeId, string newBodyType);

        Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDb(int driveTrainId, string newDrivetrain);

        Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDb(int gearboxId, string newGearbox);

        Task<Make> UpdateMakeInDb(int gearboxId, string newGearbox);

        Task<Model> UpdateModelInDb(int modelId, string newModel);

        Task<VehicleFuelType> UpdateFuelTypeInDb(int fuelTypeId, string newFuelType);

        Task<VehicleCondition> UpdateVehicleConditionInDb(int vehicleConditionId, string newVehicleCondition);

        Task<VehicleOption> UpdateVehicleOptionInDb(int vehicleOptionId, string newVehicleOption);

        Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDb(int marketVersionId, string newMarketVersion);
        
        Task<VehicleColor> DeleteVehicleColorFromDb(int colorId);

        Task<VehicleBodyType> DeleteVehicleBodyTypeFromDb(int bodyTypeId);

        Task<VehicleDrivetrainType> DeleteVehicleDrivetrainTypeFromDb(int driveTrainId);

        Task<VehicleGearboxType> DeleteVehicleGearboxTypeFromDb(int gearboxId);

        Task<Make> DeleteMakeFromDb(int makeId);

        Task<Model> DeleteModelFromDb(int modelId);

        Task<VehicleFuelType> DeleteFuelTypeFromDb(int fuelTypeId);

        Task<VehicleCondition> DeleteVehicleConditionFromDb(int vehicleConditionId);

        Task<VehicleOption> DeleteVehicleOptionFromDb(int vehicleOptionId);

        Task<VehicleMarketVersion> DeleteVehicleMarketVersionFromDb(int marketVersionId);
    }
}

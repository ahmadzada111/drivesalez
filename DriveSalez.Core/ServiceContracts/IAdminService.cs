using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.ServiceContracts
{
    public interface IAdminService
    {
        Task<VehicleColor> AddColor(string color);

        Task<VehicleBodyType> AddBodyType(string bodyType);

        Task<VehicleDrivetrainType> AddVehicleDrivetrainType(string driveTrainType);

        Task<VehicleGearboxType> AddVehicleGearboxType(string gearboxType);

        Task<Make> AddMake(string make);

        Task<Model> AddModel(int makeId, string model);

        Task<VehicleFuelType> AddVehicleFuelType(string fuelType);

        Task<VehicleCondition> AddVehicleCondition(string condition);

        Task<VehicleMarketVersion> AddVehicleMarketVersion(string marketVersion);

        Task<VehicleOption> AddVehicleOption(string option);

        Task<VehicleColor> UpdateVehicleColor(int colorId, string newColor);

        Task<VehicleBodyType> UpdateVehicleBodyType(int bodyTypeId, string newBodyType);

        Task<VehicleDrivetrainType> UpdateVehicleDrivetrainType(int drivetrainId, string newDrivetrain);

        Task<VehicleGearboxType> UpdateVehicleGearboxType(int gearboxId, string newGearbox);

        Task<Make> UpdateMake(int makeId, string newMake);

        Task<Model> UpdateModel(int modelId, string newModel);

        Task<VehicleFuelType> UpdateFuelType(int fuelTypeId, string newFuelType);

        Task<VehicleCondition> UpdateVehicleCondition(int vehicleConditionId, string newVehicleCondition);

        Task<VehicleOption> UpdateVehicleOption(int vehicleOptionId, string newVehicleOption);

        Task<VehicleMarketVersion> UpdateVehicleMarketVersion(int marketVersionId, string newMarketVersion);

        Task<VehicleColor> DeleteVehicleColor(int colorId);

        Task<VehicleBodyType> DeleteVehicleBodyType(int bodyTypeId);

        Task<VehicleDrivetrainType> DeleteVehicleDrivetrainType(int drivetrainId);

        Task<VehicleGearboxType> DeleteVehicleGearboxType(int gearboxId);

        Task<Make> DeleteMake(int makeId);

        Task<Model> DeleteModel(int modelId);

        Task<VehicleFuelType> DeleteFuelType(int fuelTypeId);

        Task<VehicleCondition> DeleteVehicleCondition(int vehicleConditionId);

        Task<VehicleOption> DeleteVehicleOption(int vehicleOptionId);

        Task<VehicleMarketVersion> DeleteVehicleMarketVersion(int marketVersionId);
        
        Task<ApplicationUser> AddModerator(RegisterDto registerDTO);
    }
}

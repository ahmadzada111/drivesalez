﻿using DriveSalez.Core.DTO;
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
        Task<VehicleColor> SendNewColorToDbAsync(string color);

        Task<VehicleBodyType> SendNewBodyTypeToDbAsync(string bodyType);

        Task<VehicleDrivetrainType> SendNewVehicleDrivetrainTypeToDbAsync(string driveTrainType);

        Task<VehicleGearboxType> SendNewVehicleGearboxTypeToDbAsync(string gearboxType);

        Task<Make> SendNewMakeToDbAsync(string make);

        Task<Model> SendNewModelToDbAsync(int makeId, string model);

        Task<VehicleFuelType> SendNewVehicleFuelTypeToDbAsync(string fuelType);

        Task<VehicleCondition> SendNewVehicleDetailsConditionToDbAsync(string condition);

        Task<VehicleMarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion);

        Task<VehicleOption> SendNewVehicleDetailsOptionsToDbAsync(string option);

        Task<VehicleColor> UpdateVehicleColorInDbAsync(int colorId, string newColor);

        Task<VehicleBodyType> UpdateVehicleBodyTypeInDbAsync(int bodyTypeId, string newBodyType);

        Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDbAsync(int driveTrainId, string newDrivetrain);

        Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDbAsync(int gearboxId, string newGearbox);

        Task<Make> UpdateMakeInDbAsync(int gearboxId, string newGearbox);

        Task<Model> UpdateModelInDbAsync(int modelId, string newModel);

        Task<VehicleFuelType> UpdateFuelTypeInDbAsync(int fuelTypeId, string newFuelType);

        Task<VehicleCondition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition);

        Task<VehicleOption> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption);

        Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion);
        
        Task<VehicleColor> DeleteVehicleColorFromDbAsync(int colorId);

        Task<VehicleBodyType> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId);

        Task<VehicleDrivetrainType> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId);

        Task<VehicleGearboxType> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId);

        Task<Make> DeleteMakeFromDbAsync(int makeId);

        Task<Model> DeleteModelFromDbAsync(int modelId);

        Task<VehicleFuelType> DeleteFuelTypeFromDbAsync(int fuelTypeId);

        Task<VehicleCondition> DeleteVehicleConditionFromDbAsync(int vehicleConditionId);

        Task<VehicleOption> DeleteVehicleOptionFromDbAsync(int vehicleOptionId);

        Task<VehicleMarketVersion> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId);
    }
}
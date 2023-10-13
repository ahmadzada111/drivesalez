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

        VehicleDriveTrainType SendNewVehicleDriveTrainTypeToDb(string driveTrainType);

        VehicleGearboxType SendNewVehicleGearboxTypeToDb(string gearboxType);

        Make SendNewMakeToDb(string make);

        Model SendNewModelToDb(int makeId, string model);

        VehicleFuelType SendNewVehicleFuelTypeToDb(string fuelType);

        VehicleDetailsCondition SendNewVehicleDetailsConditionToDb(string condition);

        VehicleMarketVersion SendNewVehicleMarketVersionToDb(string marketVersion);

        VehicleDetailsOptions SendNewVehicleDetailsOptionsToDb(string option);
       
        IEnumerable<VehicleColor> GetAllColorsFromDb();

        IEnumerable<VehicleBodyType> GetAllVehicleBodyTypesFromDb();
        
        IEnumerable<VehicleDriveTrainType> GetAllVehicleDriveTrainsFromDb();
        
        IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypesFromDb();
        
        IEnumerable<Make> GetAllMakesFromDb();
        
        IEnumerable<VehicleFuelType> GetAllVehicleFuelTypesFromDb();
        
        IEnumerable<VehicleDetailsCondition> GetAllVehicleDetailsConditionsFromDb();
        
        IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersionsFromDb();
        
        IEnumerable<VehicleDetailsOptions> GetAllVehicleDetailsOptionsFromDb();

        IEnumerable<Model> GetAllModelsByMakeIdFromDb(int id);
    }
}

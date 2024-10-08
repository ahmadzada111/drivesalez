using DriveSalez.Domain.Entities;

namespace DriveSalez.Persistence.Contracts.ServiceContracts;

public interface ICarQueryService
{
    Task<IEnumerable<Make>> GetAllMakesAsync();
    
    Task<IEnumerable<Model>> GetModelsByMakeAsync(string makeName);

    Task<IEnumerable<BodyType>> GetBodyTypesAsync();
    
    Task<IEnumerable<FuelType>> GetFuelTypesAsync();

    Task<IEnumerable<DrivetrainType>> GetDrivetrainTypesAsync();
}

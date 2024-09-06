using DriveSalez.SharedKernel.DTO.FuelTypeDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IFuelTypeService
{
    Task<FuelTypeDto> CreateFuelType(string type);

    Task<IEnumerable<FuelTypeDto>> GetAllFuelTypes();

    Task<FuelTypeDto> FindFuelTypeById(int id);

    Task<FuelTypeDto> UpdateFuelType(FuelTypeDto fuelTypeDto);
    
    Task<bool> DeleteFuelType(int id);
}
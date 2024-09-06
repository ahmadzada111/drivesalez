using DriveSalez.SharedKernel.DTO.GearboxTypeDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IGearboxTypeService
{
    Task<GearboxTypeDto> CreateGearboxType(string type);

    Task<IEnumerable<GearboxTypeDto>> GetAllGearboxTypes();

    Task<GearboxTypeDto> FindGearboxTypeById(int id);

    Task<GearboxTypeDto> UpdateGearboxType(GearboxTypeDto gearboxTypeDto);
    
    Task<bool> DeleteGearboxType(int id);
}
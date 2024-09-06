using DriveSalez.SharedKernel.DTO.ManufactureYearDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IManufactureYearService
{
    Task<ManufactureYearDto> CreateManufactureYear(int year);
    
    Task<IEnumerable<ManufactureYearDto>> GetAllManufactureYears();

    Task<ManufactureYearDto> FindManufactureYearById(int id);

    Task<ManufactureYearDto> UpdateManufactureYear(ManufactureYearDto manufactureYearDto);
    
    Task<bool> DeleteManufactureYear(int id);
}
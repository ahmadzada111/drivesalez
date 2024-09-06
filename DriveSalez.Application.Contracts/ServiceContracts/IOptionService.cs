using DriveSalez.SharedKernel.DTO.OptionDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IOptionService
{
    Task<OptionDto> CreateOption(string title);

    Task<IEnumerable<OptionDto>> GetAllOptions();

    Task<OptionDto> FindOptionById(int id);

    Task<OptionDto> UpdateOption(OptionDto optionDto);
    
    Task<bool> DeleteOption(int id);
}
using DriveSalez.SharedKernel.DTO.ColorDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IColorService
{
    Task<ColorDto> CreateColor(string color);

    Task<IEnumerable<ColorDto>> GetAllColors();

    Task<ColorDto> FindColorById(int id);

    Task<ColorDto> UpdateColor(ColorDto colorDto);
    
    Task<bool> DeleteColor(int id);
}
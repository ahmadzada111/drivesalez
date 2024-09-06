using DriveSalez.SharedKernel.DTO.BodyTypeDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IBodyTypeService
{
    Task<BodyTypeDto> CreateBodyType(string type);

    Task<IEnumerable<BodyTypeDto>> GetAllBodyTypes();

    Task<BodyTypeDto> FindBodyTypeById(int id);

    Task<BodyTypeDto> UpdateBodyType(BodyTypeDto bodyTypeDto);
    
    Task<bool> DeleteBodyType(int id);
}
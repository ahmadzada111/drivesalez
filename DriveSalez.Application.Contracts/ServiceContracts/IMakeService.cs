using DriveSalez.SharedKernel.DTO.MakeDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IMakeService
{
    Task<GetMakeDto> CreateMake(string title);

    Task<GetMakeDto> CreateMake(CreateMakeDto makeDto);

    Task<IEnumerable<GetMakeDto>> GetAllMakes();

    Task<GetMakeDto> FindMakeById(int id);

    Task<GetMakeDto> UpdateMakes(UpdateMakeDto makeDto);
    
    Task<bool> DeleteMake(int id);
}
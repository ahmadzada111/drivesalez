using DriveSalez.SharedKernel.DTO.MakeDTO;
using DriveSalez.SharedKernel.DTO.ModelDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IModelService
{
    Task<ModelDto> CreatModel(CreateModelDto modelDto);

    Task<IEnumerable<ModelDto>> GetAllModels();

    Task<ModelDto> FindModelById(int id);

    Task<ModelDto> UpdateModel(ModelDto modelDto);
    
    Task<bool> DeleteModel(int id);
}
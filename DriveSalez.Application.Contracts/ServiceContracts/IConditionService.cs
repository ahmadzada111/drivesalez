using DriveSalez.SharedKernel.DTO.ConditionDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IConditionService
{
    Task<ConditionDto> CreateCondition(CreateConditionDto conditionDto);

    Task<IEnumerable<ConditionDto>> GetAllConditions();

    Task<ConditionDto> FindConditionById(int id);
    
    Task<ConditionDto> UpdateCondition(ConditionDto conditionDto);
    
    Task<bool> DeleteCondition(int id);
}
using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.ConditionDTO;

namespace DriveSalez.Application.Services;

internal class ConditionService : IConditionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ConditionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ConditionDto> CreateCondition(CreateConditionDto conditionDto)
    {
        var result = _unitOfWork.Conditions.Add(
            new Condition
            {
                Title = conditionDto.Title, 
                Description = conditionDto.Description
            });

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ConditionDto>(result);
    }

    public async Task<IEnumerable<ConditionDto>> GetAllConditions()
    {
        var conditions = await _unitOfWork.Conditions.GetAll();
        return _mapper.Map<IEnumerable<ConditionDto>>(conditions);
    }

    public async Task<ConditionDto> FindConditionById(int id)
    {
        var condition = await _unitOfWork.Conditions.FindById(id);
        return _mapper.Map<ConditionDto>(condition);
    }

    public async Task<ConditionDto> UpdateCondition(ConditionDto conditionDto)
    {
        var conditionToUpdate = await _unitOfWork.Conditions.FindById(conditionDto.Id);
        conditionToUpdate.Description = conditionDto.Description;
        conditionToUpdate.Title = conditionDto.Title;
        _unitOfWork.Conditions.Update(conditionToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ConditionDto>(conditionToUpdate);
    }

    public async Task<bool> DeleteCondition(int id)
    {
        var conditionToDelete = await _unitOfWork.Conditions.FindById(id);
        _unitOfWork.Conditions.Delete(conditionToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
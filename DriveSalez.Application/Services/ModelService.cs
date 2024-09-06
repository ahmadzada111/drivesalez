using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.ModelDTO;

namespace DriveSalez.Application.Services;

internal class ModelService : IModelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ModelDto> CreatModel(CreateModelDto modelDto)
    {
        var model = _unitOfWork.Models.Add(new Model { Title = modelDto.Title });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ModelDto>(model);
    }

    public async Task<IEnumerable<ModelDto>> GetAllModels()
    {
        var models = await _unitOfWork.Models.GetAll();
        return _mapper.Map<IEnumerable<ModelDto>>(models);
    }

    public async Task<ModelDto> FindModelById(int id)
    {
        var model = await _unitOfWork.Models.FindById(id);
        return _mapper.Map<ModelDto>(model);
    }

    public async Task<ModelDto> UpdateModel(ModelDto modelDto)
    {
        var modelToUpdate = await _unitOfWork.Models.FindById(modelDto.Id);
        modelToUpdate.Title = modelDto.Title;
        _unitOfWork.Models.Update(modelToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ModelDto>(modelToUpdate);
    }

    public async Task<bool> DeleteModel(int id)
    {
        var modelToDelete = await _unitOfWork.Models.FindById(id);
        _unitOfWork.Models.Delete(modelToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
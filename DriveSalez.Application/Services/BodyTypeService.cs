using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.BodyTypeDTO;

namespace DriveSalez.Application.Services;

internal class BodyTypeService : IBodyTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BodyTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BodyTypeDto> CreateBodyType(string type)
    {
        var bodyType = _unitOfWork.BodyTypes.Add(new BodyType { Type = type });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BodyTypeDto>(bodyType);
    }

    public async Task<IEnumerable<BodyTypeDto>> GetAllBodyTypes()
    {
        var bodyTypes = await _unitOfWork.BodyTypes.GetAll();
        return _mapper.Map<IEnumerable<BodyTypeDto>>(bodyTypes);
    }

    public async Task<BodyTypeDto> FindBodyTypeById(int id)
    {
        var bodyType = await _unitOfWork.BodyTypes.FindById(id);
        return _mapper.Map<BodyTypeDto>(bodyType);
    }

    public async Task<BodyTypeDto> UpdateBodyType(BodyTypeDto bodyTypeDto)
    {
        var bodyTypeToUpdate = await _unitOfWork.BodyTypes.FindById(bodyTypeDto.Id);
        bodyTypeToUpdate.Type = bodyTypeDto.Type;
        _unitOfWork.BodyTypes.Update(bodyTypeToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<BodyTypeDto>(bodyTypeToUpdate);
    }

    public async Task<bool> DeleteBodyType(int id)
    {
        var bodyTypeToDelete = await _unitOfWork.BodyTypes.FindById(id);
        _unitOfWork.BodyTypes.Delete(bodyTypeToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
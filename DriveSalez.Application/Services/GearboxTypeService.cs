using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.GearboxTypeDTO;

namespace DriveSalez.Application.Services;

internal class GearboxTypeService : IGearboxTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GearboxTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GearboxTypeDto> CreateGearboxType(string type)
    {
        var gearbox = _unitOfWork.GearboxTypes.Add(new GearboxType() { Type = type });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GearboxTypeDto>(gearbox);
    }

    public async Task<IEnumerable<GearboxTypeDto>> GetAllGearboxTypes()
    {
        var gearboxes = await _unitOfWork.GearboxTypes.GetAll();
        return _mapper.Map<IEnumerable<GearboxTypeDto>>(gearboxes);
    }

    public async Task<GearboxTypeDto> FindGearboxTypeById(int id)
    {
        var gearbox = await _unitOfWork.GearboxTypes.FindById(id);
        return _mapper.Map<GearboxTypeDto>(gearbox);
    }

    public async Task<GearboxTypeDto> UpdateGearboxType(GearboxTypeDto gearboxTypeDto)
    {
        var gearboxToUpdate = await _unitOfWork.GearboxTypes.FindById(gearboxTypeDto.Id);
        gearboxToUpdate.Type = gearboxTypeDto.Type;
        _unitOfWork.GearboxTypes.Update(gearboxToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GearboxTypeDto>(gearboxToUpdate);
    }

    public async Task<bool> DeleteGearboxType(int id)
    {
        var gearboxToDelete = await _unitOfWork.GearboxTypes.FindById(id);
        _unitOfWork.GearboxTypes.Delete(gearboxToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
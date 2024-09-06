using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.FuelTypeDTO;

namespace DriveSalez.Application.Services;

internal class FuelTypeService : IFuelTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FuelTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FuelTypeDto> CreateFuelType(string type)
    {
        var fuelType = _unitOfWork.FuelTypes.Add(new FuelType { Type = type });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<FuelTypeDto>(fuelType);
    }

    public async Task<IEnumerable<FuelTypeDto>> GetAllFuelTypes()
    {
        var fuelTypes = await _unitOfWork.FuelTypes.GetAll();
        return _mapper.Map<IEnumerable<FuelTypeDto>>(fuelTypes);
    }

    public async Task<FuelTypeDto> FindFuelTypeById(int id)
    {
        var fuelType = await _unitOfWork.FuelTypes.FindById(id);
        return _mapper.Map<FuelTypeDto>(fuelType);
    }

    public async Task<FuelTypeDto> UpdateFuelType(FuelTypeDto fuelTypeDto)
    {
        var fuelTypeToUpdate = await _unitOfWork.FuelTypes.FindById(fuelTypeDto.Id);
        fuelTypeToUpdate.Type = fuelTypeDto.Type;
        _unitOfWork.FuelTypes.Update(fuelTypeToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<FuelTypeDto>(fuelTypeToUpdate);
    }

    public async Task<bool> DeleteFuelType(int id)
    {
        var fuelTypeToDelete = await _unitOfWork.FuelTypes.FindById(id);
        _unitOfWork.FuelTypes.Delete(fuelTypeToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
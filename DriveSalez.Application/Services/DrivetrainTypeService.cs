using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.DrivetrainTypeDTO;

namespace DriveSalez.Application.Services;

internal class DrivetrainTypeService : IDrivetrainTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DrivetrainTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DrivetrainTypeDto> CreateDrivetrainType(string type)
    {
        var drivetrain = _unitOfWork.DrivetrainTypes.Add(new DrivetrainType { Type = type });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<DrivetrainTypeDto>(drivetrain);
    }

    public async Task<IEnumerable<DrivetrainTypeDto>> GetDrivetrainTypes()
    {
        var drivetrains = await _unitOfWork.DrivetrainTypes.GetAll();
        return _mapper.Map<IEnumerable<DrivetrainTypeDto>>(drivetrains);
    }

    public async Task<DrivetrainTypeDto> FindDrivetrainTypeById(int id)
    {
        var drivetrain = await _unitOfWork.DrivetrainTypes.FindById(id);
        return _mapper.Map<DrivetrainTypeDto>(drivetrain);
    }

    public async Task<DrivetrainTypeDto> UpdateDrivetrainType(DrivetrainTypeDto drivetrainTypeDto)
    {
        var drivetrainToUpdate = await _unitOfWork.DrivetrainTypes.FindById(drivetrainTypeDto.Id);
        drivetrainToUpdate.Type = drivetrainTypeDto.Type;
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<DrivetrainTypeDto>(drivetrainToUpdate);
    }

    public async Task<bool> DeleteDrivetrainType(int id)
    {
        var drivetrainToDelete = await _unitOfWork.DrivetrainTypes.FindById(id);
        _unitOfWork.DrivetrainTypes.Delete(drivetrainToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.ManufactureYearDTO;

namespace DriveSalez.Application.Services;

internal class ManufactureYearService : IManufactureYearService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ManufactureYearService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ManufactureYearDto> CreateManufactureYear(int year)
    {
        var manufactureYear = _unitOfWork.ManufactureYears.Add(new ManufactureYear { Year = year });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ManufactureYearDto>(manufactureYear);
    }

    public async Task<IEnumerable<ManufactureYearDto>> GetAllManufactureYears()
    {
        var manufactureYears = await _unitOfWork.ManufactureYears.GetAll();
        return _mapper.Map<IEnumerable<ManufactureYearDto>>(manufactureYears);
    }

    public async Task<ManufactureYearDto> FindManufactureYearById(int id)
    {
        var manufactureYear = await _unitOfWork.ManufactureYears.FindById(id);
        return _mapper.Map<ManufactureYearDto>(manufactureYear);
    }

    public async Task<ManufactureYearDto> UpdateManufactureYear(ManufactureYearDto manufactureYearDto)
    {
        var manufactureYearToUpdate = await _unitOfWork.ManufactureYears.FindById(manufactureYearDto.Id);
        manufactureYearToUpdate.Year = manufactureYearDto.Year;
        _unitOfWork.ManufactureYears.Update(manufactureYearToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ManufactureYearDto>(manufactureYearToUpdate);
    }

    public async Task<bool> DeleteManufactureYear(int id)
    {
        var manufactureYearToDelete = await _unitOfWork.ManufactureYears.FindById(id);
        _unitOfWork.ManufactureYears.Delete(manufactureYearToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.ColorDTO;

namespace DriveSalez.Application.Services;

internal class ColorService : IColorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ColorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ColorDto> CreateColor(string color)
    {
        var result = _unitOfWork.Colors.Add(new Color { Title = color });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ColorDto>(result);
    }

    public async Task<IEnumerable<ColorDto>> GetAllColors()
    {
        var result = await _unitOfWork.Colors.GetAll();
        return _mapper.Map<IEnumerable<ColorDto>>(result);
    }

    public async Task<ColorDto> FindColorById(int id)
    {
        var result = await _unitOfWork.Colors.FindById(id);
        return _mapper.Map<ColorDto>(result);
    }

    public async Task<ColorDto> UpdateColor(ColorDto colorDto)
    {
        var colorToUpdate = await _unitOfWork.Colors.FindById(colorDto.Id);
        colorToUpdate.Title = colorDto.Title;
        _unitOfWork.Colors.Update(colorToUpdate);
        return _mapper.Map<ColorDto>(colorToUpdate);
    }

    public async Task<bool> DeleteColor(int id)
    {
        var colorToDelete = await _unitOfWork.Colors.FindById(id);
        _unitOfWork.Colors.Delete(colorToDelete);
        return true;
    }
}
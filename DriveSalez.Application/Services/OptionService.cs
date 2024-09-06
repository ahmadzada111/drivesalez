using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.OptionDTO;

namespace DriveSalez.Application.Services;

internal class OptionService : IOptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OptionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OptionDto> CreateOption(string title)
    {
        var option = _unitOfWork.Options.Add(new Option { Title = title });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OptionDto>(option);
    }

    public async Task<IEnumerable<OptionDto>> GetAllOptions()
    {
        var options = await _unitOfWork.Options.GetAll();
        return _mapper.Map<IEnumerable<OptionDto>>(options);
    }

    public async Task<OptionDto> FindOptionById(int id)
    {
        var option = await _unitOfWork.Options.FindById(id);
        return _mapper.Map<OptionDto>(option);
    }

    public async Task<OptionDto> UpdateOption(OptionDto optionDto)
    {
        var optionToUpdate = await _unitOfWork.Options.FindById(optionDto.Id);
        optionToUpdate.Title = optionDto.Title;
        _unitOfWork.Options.Update(optionToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<OptionDto>(optionToUpdate);
    }

    public async Task<bool> DeleteOption(int id)
    {
        var optionToDelete = await _unitOfWork.Options.FindById(id);
        _unitOfWork.Options.Delete(optionToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
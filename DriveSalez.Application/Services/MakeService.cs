using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.MakeDTO;

namespace DriveSalez.Application.Services;

internal class MakeService : IMakeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MakeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetMakeDto> CreateMake(string title)
    {
        var make = _unitOfWork.Makes.Add(new Make { Title = title });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetMakeDto>(make);
    }

    public async Task<GetMakeDto> CreateMake(CreateMakeDto makeDto)
    {
        var make = _unitOfWork.Makes.Add(new Make { Title = makeDto.Title });
        
        foreach (var model in makeDto.Models)
        {
            _unitOfWork.Models.Add(new Model { Title = model, Make = make });
        }
        
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetMakeDto>(make);
    }

    public async Task<IEnumerable<GetMakeDto>> GetAllMakes()
    {
        var makes = await _unitOfWork.Makes.GetAll();
        return _mapper.Map<IEnumerable<GetMakeDto>>(makes);
    }

    public async Task<GetMakeDto> FindMakeById(int id)
    {
        var make = await _unitOfWork.Makes.FindById(id);
        return _mapper.Map<GetMakeDto>(make);
    }

    public async Task<GetMakeDto> UpdateMakes(UpdateMakeDto makeDto)
    {
        var makeToUpdate = await _unitOfWork.Makes.FindById(makeDto.Id);
        makeToUpdate.Title = makeDto.Title;
        _unitOfWork.Makes.Update(makeToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetMakeDto>(makeToUpdate);
    }

    public Task<bool> DeleteMake(int id)
    {
        throw new NotImplementedException();
    }
}
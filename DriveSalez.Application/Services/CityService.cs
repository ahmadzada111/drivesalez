using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.CityDTO;

namespace DriveSalez.Application.Services;

internal class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CityService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCityDto> CreateCity(CreateCityDto cityDto)
    {
        var country = await _unitOfWork.Countries.FindById(cityDto.CountryId);
        var city = _unitOfWork.Cities.Add(new City { Name = cityDto.Name, Country = country});
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetCityDto>(city);
    }

    public async Task<IEnumerable<GetCityDto>> GetCities()
    {
        var cities = await _unitOfWork.Cities.GetAll();
        return _mapper.Map<IEnumerable<GetCityDto>>(cities);
    }

    public async Task<GetCityDto> FindCityById(int id)
    {
        var city = await _unitOfWork.Cities.FindById(id);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetCityDto>(city);
    }

    public async Task<GetCityDto> UpdateCity(UpdateCityDto cityDto)
    {
        var cityToUpdate = await _unitOfWork.Cities.FindById(cityDto.Id);
        cityToUpdate.Name = cityDto.Name;
        _unitOfWork.Cities.Update(cityToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetCityDto>(cityToUpdate);
    }

    public async Task<bool> DeleteCity(int id)
    {
        var cityToDelete = await _unitOfWork.Cities.FindById(id);
        _unitOfWork.Cities.Delete(cityToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
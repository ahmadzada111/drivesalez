using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.CountryDTO;

namespace DriveSalez.Application.Services;

internal class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetCountryDto> CreateCountry(string name)
    {
        var country = _unitOfWork.Countries.Add(new Country { Name = name });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetCountryDto>(country);
    }

    public async Task<GetCountryDto> CreateCountry(CreateCountryDto countryDto)
    {
        var country = _unitOfWork.Countries.Add(new Country { Name = countryDto.Name });
        
        foreach (var city in countryDto.Cities)
        {
            _unitOfWork.Cities.Add(new City { Name = city, Country = country });
        }
        
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetCountryDto>(country);
    }

    public async Task<IEnumerable<GetCountryDto>> GetCountries()
    {
        var countries = await _unitOfWork.Countries.GetAll();
        return _mapper.Map<IEnumerable<GetCountryDto>>(countries);
    }

    public async Task<GetCountryDto> FindCountryById(int id)
    {
        var country = await _unitOfWork.Countries.FindById(id);
        return _mapper.Map<GetCountryDto>(country);
    }

    public async Task<GetCountryDto> UpdateCountry(UpdateCountryDto countryDto)
    {
        var countryToUpdate = await _unitOfWork.Countries.FindById(countryDto.Id);
        countryToUpdate.Name = countryDto.Name;
        _unitOfWork.Countries.Update(countryToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetCountryDto>(countryToUpdate);
    }

    public async Task<bool> DeleteCountry(int id)
    {
        var countryToDelete = await _unitOfWork.Countries.FindById(id);
        _unitOfWork.Countries.Delete(countryToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.RepositoryContracts;

namespace DriveSalez.Application.Services;

public class DetailsService : IDetailsService
{
    private readonly IDetailsRepository _detailsRepository;
    private readonly IMapper _mapper;

    public DetailsService(IDetailsRepository detailsRepository, IMapper mapper) 
    {
        _detailsRepository = detailsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ColorDto>> GetAllColorsAsync()
    {
        var response = await _detailsRepository.GetAllColorsFromDbAsync();
        return _mapper.Map<List<ColorDto>>(response);
    }

    public async Task<IEnumerable<BodyTypeDto>> GetAllVehicleBodyTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();
        return _mapper.Map<List<BodyTypeDto>>(response);
    }

    public async Task<IEnumerable<DrivetrainTypeDto>> GetAllVehicleDrivetrainsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();
        return _mapper.Map<List<DrivetrainTypeDto>>(response);
    }

    public async Task<IEnumerable<GearboxTypeDto>> GetAllVehicleGearboxTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();
        return _mapper.Map<List<GearboxTypeDto>>(response);
    }

    public async Task<IEnumerable<MakeDto>> GetAllMakesAsync()
    {
        var response = await _detailsRepository.GetAllMakesFromDbAsync();
        return _mapper.Map<List<MakeDto>>(response);
    }

    public async Task<IEnumerable<FuelTypeDto>> GetAllVehicleFuelTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();
        return _mapper.Map<List<FuelTypeDto>>(response);
    }

    public async Task<IEnumerable<ConditionDto>> GetAllVehicleDetailsConditionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDetailsConditionsFromDbAsync();
        return _mapper.Map<List<ConditionDto>>(response);
    }

    public async Task<IEnumerable<AnnouncementTypePricingDto>> GetAllSubscriptionsAsync()
    {
        var response = await _detailsRepository.GetAllSubscriptionsFromDbAsync();
        return _mapper.Map<List<AnnouncementTypePricingDto>>(response);
    }

    public async Task<IEnumerable<AnnouncementTypePricingDto>> GetAllAnnouncementPricingsAsync()
    {
        var response = await _detailsRepository.GetAllAnnouncementTypePricingsFromDbAsync();
        return _mapper.Map<List<AnnouncementTypePricingDto>>(response);
    }
    
    public async Task<IEnumerable<CityDto>> GetAllCitiesByCountryIdAsync(int countryId)
    {
        var response = await _detailsRepository.GetAllCitiesByCountryIdFromDbAsync(countryId);
        return _mapper.Map<List<CityDto>>(response);
    }

    public async Task<IEnumerable<MarketVersionDto>> GetAllVehicleMarketVersionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();
        return _mapper.Map<List<MarketVersionDto>>(response);
    }

    public async Task<IEnumerable<OptionDto>> GetAllVehicleDetailsOptionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDetailsOptionsFromDbAsync();
        return _mapper.Map<List<OptionDto>>(response);
    }

    public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
    {
        var response = await _detailsRepository.GetAllModelsFromDbAsync();
        return _mapper.Map<List<ModelDto>>(response);
    }
    
    public async Task<IEnumerable<ModelDto>> GetAllModelsByMakeIdAsync(int id)
    {
        var response = await _detailsRepository.GetAllModelsByMakeIdFromDbAsync(id);
        return _mapper.Map<List<ModelDto>>(response);
    }
    
    public async Task<IEnumerable<ManufactureYearDto>> GetAllManufactureYearsAsync()
    {
        var response = await _detailsRepository.GetAllManufactureYearsFromDbAsync();
        return _mapper.Map<List<ManufactureYearDto>>(response);
    }
    
    public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync()
    {
        var response = await _detailsRepository.GetAllCountriesFromDbAsync();
        return _mapper.Map<List<CountryDto>>(response);
    }
    
    public async Task<IEnumerable<CityDto>> GetAllCitiesAsync()
    {
        var response = await _detailsRepository.GetAllCitiesFromDbAsync();
        return _mapper.Map<List<CityDto>>(response);
    }
}
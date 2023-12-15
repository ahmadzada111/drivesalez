using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class DetailsController : Controller
{
    private readonly IDetailsService _detailsService;

    public DetailsController(IDetailsService detailsService)
    {
        _detailsService = detailsService;
    }

    [HttpGet("get-all-colors")]
    public async Task<ActionResult> GetAllColors()
    {
        var response = await _detailsService.GetAllColorsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-body-types")]
    public async Task<ActionResult> GetAllBodyTypes()
    {
        var response = await _detailsService.GetAllVehicleBodyTypesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-drivetrain-types")]
    public async Task<ActionResult> GetAllDrivetrainTypes()
    {
        var response = await _detailsService.GetAllVehicleDrivetrainsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-gearbox-types")]
    public async Task<ActionResult> GetAllGearboxTypes()
    {
        var response = await _detailsService.GetAllVehicleGearboxTypesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-makes")]
    public async Task<ActionResult> GetAllMakes()
    {
        var response = await _detailsService.GetAllMakesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-models")]
    public async Task<ActionResult> GetAllModels()
    {
        var response = await _detailsService.GetAllModelsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-models-by-make")]
    public async Task<ActionResult> GetAllModelsByMake([FromQuery] int id)
    {
        var response = await _detailsService.GetAllModelsByMakeIdAsync(id);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-fuel-types")]
    public async Task<ActionResult> GetAllFuelTypes()
    {
        var response = await _detailsService.GetAllVehicleFuelTypesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-conditions")]
    public async Task<ActionResult> GetAllVehicleDetailsConditions()
    {
        var response = await _detailsService.GetAllVehicleDetailsConditionsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-market-versions")]
    public async Task<ActionResult> GetAllVehicleMarketVersions()
    {
        var response = await _detailsService.GetAllVehicleMarketVersionsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-options")]
    public async Task<ActionResult> GetAllVehicleDetailsOptions()
    {
        var response = await _detailsService.GetAllVehicleDetailsOptionsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-manufacture-years")]
    public async Task<ActionResult> GetAllManufactureYears()
    {
        var response = await _detailsService.GetAllManufactureYearsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-countries")]
    public async Task<ActionResult> GetAllCountries()
    {
        var response = await _detailsService.GetAllCountriesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-cities")]
    public async Task<ActionResult> GetAllCities()
    {
        var response = await _detailsService.GetAllCitiesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-currencies")]
    public async Task<ActionResult> GetAllCurrencies()
    {
        var response = await _detailsService.GetAllCurrenciesAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-subscriptions")]
    public async Task<ActionResult> GetAllSubscriptions()
    {
        var response = await _detailsService.GetAllSubscriptionsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-cities-by-country-id")]
    public async Task<ActionResult> GetAllCitiesByCountryId()
    {
        var response = await _detailsService.GetAllSubscriptionsAsync();
        return response != null ? Ok(response) : BadRequest(response);
    }
}
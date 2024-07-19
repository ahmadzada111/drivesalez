using DriveSalez.Application.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class DetailsController : Controller
{
    private readonly IDetailsService _detailsService;
    private readonly ILogger _logger;
    
    public DetailsController(IDetailsService detailsService, ILogger<DetailsController> logger)
    {
        _detailsService = detailsService;
        _logger = logger;
    }

    [HttpGet("get-all-colors")]
    public async Task<ActionResult> GetAllColors()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _detailsService.GetAllColorsAsync();
        return Ok(response);
    }

    [HttpGet("get-all-body-types")]
    public async Task<ActionResult> GetAllBodyTypes()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleBodyTypesAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-drivetrain-types")]
    public async Task<ActionResult> GetAllDrivetrainTypes()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleDrivetrainsAsync();
        return Ok(response);
    }

    [HttpGet("get-all-gearbox-types")]
    public async Task<ActionResult> GetAllGearboxTypes()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleGearboxTypesAsync();
        return Ok(response);
    }

    [HttpGet("get-all-makes")]
    public async Task<ActionResult> GetAllMakes()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllMakesAsync();
        return Ok(response);  
    }

    [HttpGet("get-all-models")]
    public async Task<ActionResult> GetAllModels()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllModelsAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-models-by-make")]
    public async Task<ActionResult> GetAllModelsByMake([FromQuery] int id)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllModelsByMakeIdAsync(id);
        return Ok(response);   
    }

    [HttpGet("get-all-fuel-types")]
    public async Task<ActionResult> GetAllFuelTypes()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleFuelTypesAsync();
        return Ok(response);  
    }

    [HttpGet("get-all-conditions")]
    public async Task<ActionResult> GetAllVehicleDetailsConditions()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleDetailsConditionsAsync();
        return Ok(response);  
    }

    [HttpGet("get-all-market-versions")]
    public async Task<ActionResult> GetAllVehicleMarketVersions()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleMarketVersionsAsync();
        return Ok(response);  
    }

    [HttpGet("get-all-options")]
    public async Task<ActionResult> GetAllVehicleDetailsOptions()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllVehicleDetailsOptionsAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-manufacture-years")]
    public async Task<ActionResult> GetAllManufactureYears()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllManufactureYearsAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-countries")]
    public async Task<ActionResult> GetAllCountries()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllCountriesAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-cities")]
    public async Task<ActionResult> GetAllCities()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllCitiesAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-currencies")]
    public async Task<ActionResult> GetAllCurrencies()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllCurrenciesAsync();
        return Ok(response);
    }

    [HttpGet("get-all-subscriptions")]
    public async Task<ActionResult> GetAllSubscriptions()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllSubscriptionsAsync();
        return Ok(response);   
    }
    
    [HttpGet("get-all-announcement-pricings")]
    public async Task<ActionResult> GetAllAnnouncementPricings()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllAnnouncementPricingsAsync();
        return Ok(response);
    }
    
    [HttpGet("get-all-cities-by-country-id")]
    public async Task<ActionResult> GetAllCitiesByCountryId([FromQuery] int countryId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _detailsService.GetAllCitiesByCountryIdAsync(countryId);
        return Ok(response);
    }
}
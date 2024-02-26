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
    private readonly ILogger _logger;
    
    public DetailsController(IDetailsService detailsService, ILogger<DetailsController> logger)
    {
        _detailsService = detailsService;
        _logger = logger;
    }

    [HttpGet("get-all-colors")]
    public async Task<ActionResult> GetAllColors()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
            var response = await _detailsService.GetAllColorsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-body-types")]
    public async Task<ActionResult> GetAllBodyTypes()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleBodyTypesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-drivetrain-types")]
    public async Task<ActionResult> GetAllDrivetrainTypes()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleDrivetrainsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-gearbox-types")]
    public async Task<ActionResult> GetAllGearboxTypes()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleGearboxTypesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-makes")]
    public async Task<ActionResult> GetAllMakes()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllMakesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-models")]
    public async Task<ActionResult> GetAllModels()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllModelsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-models-by-make")]
    public async Task<ActionResult> GetAllModelsByMake([FromQuery] int id)
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllModelsByMakeIdAsync(id);
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-fuel-types")]
    public async Task<ActionResult> GetAllFuelTypes()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleFuelTypesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-conditions")]
    public async Task<ActionResult> GetAllVehicleDetailsConditions()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleDetailsConditionsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-market-versions")]
    public async Task<ActionResult> GetAllVehicleMarketVersions()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleMarketVersionsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-options")]
    public async Task<ActionResult> GetAllVehicleDetailsOptions()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllVehicleDetailsOptionsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-manufacture-years")]
    public async Task<ActionResult> GetAllManufactureYears()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllManufactureYearsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-countries")]
    public async Task<ActionResult> GetAllCountries()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllCountriesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-cities")]
    public async Task<ActionResult> GetAllCities()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllCitiesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-currencies")]
    public async Task<ActionResult> GetAllCurrencies()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllCurrenciesAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }

    [HttpGet("get-all-subscriptions")]
    public async Task<ActionResult> GetAllSubscriptions()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllSubscriptionsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-announcement-pricings")]
    public async Task<ActionResult> GetAllAnnouncementPricings()
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllAnnouncementPricingsAsync();
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
    
    [HttpGet("get-all-cities-by-country-id")]
    public async Task<ActionResult> GetAllCitiesByCountryId([FromQuery] int countryId)
    {
        try
        {
            _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

            var response = await _detailsService.GetAllCitiesByCountryIdAsync(countryId);
            return Ok(response);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
}
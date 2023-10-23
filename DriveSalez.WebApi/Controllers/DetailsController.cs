using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        var response = await _detailsService.GetAllColors();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-body-types")]
    public async Task<ActionResult> GetAllBodyTypes()
    {
        var response = await _detailsService.GetAllVehicleBodyTypes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-drivetrain-types")]
    public async Task<ActionResult> GetAllDrivetrainTypes()
    {
        var response = await _detailsService.GetAllVehicleDrivetrains();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-gearbox-types")]
    public async Task<ActionResult> GetAllGearboxTypes()
    {
        var response = await _detailsService.GetAllVehicleGearboxTypes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-makes")]
    public async Task<ActionResult> GetAllMakes()
    {
        var response = await _detailsService.GetAllMakes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-models")]
    public async Task<ActionResult> GetAllModels()
    {
        var response = await _detailsService.GetAllModels();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-models-by-make")]
    public async Task<ActionResult> GetAllModelsByMake(int id)
    {
        var response = await _detailsService.GetAllModelsByMakeId(id);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-fuel-types")]
    public async Task<ActionResult> GetAllFuelTypes()
    {
        var response = await _detailsService.GetAllVehicleFuelTypes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-conditions")]
    public async Task<ActionResult> GetAllVehicleDetailsConditions()
    {
        var response = await _detailsService.GetAllVehicleDetailsConditions();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-market-versions")]
    public async Task<ActionResult> GetAllVehicleMarketVersions()
    {
        var response = await _detailsService.GetAllVehicleMarketVersions();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-options")]
    public async Task<ActionResult> GetAllVehicleDetailsOptions()
    {
        var response = await _detailsService.GetAllVehicleDetailsOptions();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-years")]
    public async Task<ActionResult> GetAllManufactureYears()
    {
        var response = await _detailsService.GetAllManufactureYears();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-countries")]
    public async Task<ActionResult> GetAllCountries()
    {
        var response = await _detailsService.GetAllCountries();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-cities")]
    public async Task<ActionResult> GetAllCities()
    {
        var response = await _detailsService.GetAllCities();
        return response != null ? Ok(response) : BadRequest(response);
    }
}
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
    public ActionResult GetAllColors()
    {
        var response = _detailsService.GetAllColors();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-body-types")]
    public ActionResult GetAllBodyTypes()
    {
        var response = _detailsService.GetAllVehicleBodyTypes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-drivetrain-types")]
    public ActionResult GetAllDrivetrainTypes()
    {
        var response = _detailsService.GetAllVehicleDrivetrains();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-gearbox-types")]
    public ActionResult GetAllGearboxTypes()
    {
        var response = _detailsService.GetAllVehicleGearboxTypes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-makes")]
    public ActionResult GetAllMakes()
    {
        var response = _detailsService.GetAllMakes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-models")]
    public ActionResult GetAllModels()
    {
        var response = _detailsService.GetAllModels();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-models-by-make")]
    public ActionResult GetAllModelsByMake(int id)
    {
        var response = _detailsService.GetAllModelsByMakeId(id);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-fuel-types")]
    public ActionResult GetAllFuelTypes()
    {
        var response = _detailsService.GetAllVehicleFuelTypes();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-conditions")]
    public ActionResult GetAllVehicleDetailsConditions()
    {
        var response = _detailsService.GetAllVehicleDetailsConditions();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-market-versions")]
    public ActionResult GetAllVehicleMarketVersions()
    {
        var response = _detailsService.GetAllVehicleMarketVersions();
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-all-options")]
    public ActionResult GetAllVehicleDetailsOptions()
    {
        var response = _detailsService.GetAllVehicleDetailsOptions();
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-years")]
    public ActionResult GetAllManufactureYears()
    {
        var response = _detailsService.GetAllManufactureYears();
        return response != null ? Ok(response) : BadRequest(response);
    }
}
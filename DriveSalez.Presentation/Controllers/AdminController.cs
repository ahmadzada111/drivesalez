using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly ILogger _logger;
    
    public AdminController(IAdminService adminService, ILogger<AdminController> logger)
    {
        _adminService = adminService;
        _logger = logger;
    }

    [HttpPost("add-new-color")]
    public async Task<ActionResult> AddNewColor([FromBody] string color)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _adminService.AddColorAsync(color);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpPost("add-new-condition")]
    public async Task<ActionResult> AddNewVehicleCondition([FromBody] AddNewConditionDto request)
    {
        var response = await _adminService.AddVehicleConditionAsync(request.Condition, request.Description);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpPost("add-new-market-version")]
    public async Task<ActionResult> AddNewVehicleMarketVersion([FromBody] string marketVersion)
    {
        var response = await _adminService.AddVehicleMarketVersionAsync(marketVersion);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpPost("add-new-option")]
    public async Task<ActionResult> AddNewVehicleOption([FromBody] string option)
    {
        var response = await _adminService.AddVehicleOptionAsync(option);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpPut("update-color")]
    public async Task<ActionResult> UpdateVehicleColor([FromBody] UpdateColorDto request)
    {
        var response = await _adminService.UpdateVehicleColorAsync(request.ColorId, request.NewColor);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpPut("update-account-limit")]
    public async Task<ActionResult> UpdateUserLimit([FromBody] UpdateAccountLimitDto request)
    {
        var response = await _adminService.UpdateAccountLimitAsync(request.LimitId, request.PremiumLimit, request.RegularLimit);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpPut("update-condition")]
    public async Task<ActionResult> UpdateVehicleCondition([FromBody] UpdateConditionDto request)
    {
        var response = await _adminService.UpdateVehicleConditionAsync(request.ConditionId, request.NewCondition, request.NewDescription);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpPut("update-option")]
    public async Task<ActionResult> UpdateVehicleOption([FromBody] UpdateOptionDto request)
    {
        var response = await _adminService.UpdateVehicleOptionAsync(request.OptionId, request.NewVehicleOption);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpPut("update-market-version")]
    public async Task<ActionResult> UpdateVehicleMarketVersion([FromBody] UpdateMarketVersionDto request)
    {
        var response = await _adminService.UpdateVehicleMarketVersionAsync(request.MarketVersionId, request.NewMarketVersion);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-color")]
    public async Task<ActionResult> DeleteVehicleColor([FromBody] int colorId)
    {
        var response = await _adminService.DeleteVehicleColorAsync(colorId); 
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-body")]
    public async Task<ActionResult> DeleteVehicleBodyType([FromBody] int bodyTypeId)
    {
        var response = await _adminService.DeleteVehicleBodyTypeAsync(bodyTypeId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-country")]
    public async Task<ActionResult> DeleteCountry([FromBody] int countryId)
    {
        var response = await _adminService.DeleteCountryAsync(countryId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-city")]
    public async Task<ActionResult> DeleteCity([FromBody] int cityId)
    {
        var response = await _adminService.DeleteCityAsync(cityId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-drivetrain")]
    public async Task<ActionResult> DeleteVehicleDrivetrainType([FromBody] int drivetrainId)
    {
        var response = await _adminService.DeleteVehicleDrivetrainTypeAsync(drivetrainId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-gearbox")]
    public async Task<ActionResult> DeleteVehicleGearboxType([FromBody] int gearboxId)
    {
        var response = await _adminService.DeleteVehicleGearboxTypeAsync(gearboxId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-make")]
    public async Task<ActionResult> DeleteMake([FromBody] int makeId)
    {
        var response = await _adminService.DeleteMakeAsync(makeId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-model")]
    public async Task<ActionResult> DeleteModel([FromBody] int modelId)
    {
        var response = await _adminService.DeleteModelAsync(modelId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-fuel")]
    public async Task<ActionResult> DeleteFuelType([FromBody] int fuelTypeId)
    {
        var response = await _adminService.DeleteFuelTypeAsync(fuelTypeId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-condition")]
    public async Task<ActionResult> DeleteVehicleCondition([FromBody] int vehicleConditionId)
    {
        var response = await _adminService.DeleteVehicleConditionAsync(vehicleConditionId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-option")]
    public async Task<ActionResult> DeleteVehicleOption([FromBody] int vehicleConditionId)
    {
        var response = await _adminService.DeleteVehicleOptionAsync(vehicleConditionId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpDelete("delete-market-version")]
    public async Task<ActionResult> DeleteVehicleMarketVersion([FromBody] int marketVersionId)
    {
        var response = await _adminService.DeleteVehicleMarketVersionAsync(marketVersionId);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpPost("create-moderator")]
    public async Task<ActionResult> CreateModerator([FromBody] RegisterModeratorDto request)
    {
        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var result = await _adminService.AddModeratorAsync(request);
        return result != null ? Ok() : BadRequest();
    }

    [HttpDelete("delete-moderator")]
    public async Task<ActionResult> DeleteModerator([FromBody] Guid moderatorId)
    {
        var result = await _adminService.DeleteModeratorAsync(moderatorId);
        return result != null ? Ok(result) : BadRequest(result);
    }
    
    [HttpGet("get-all-moderators")]
    public async Task<ActionResult> GetAllModerators(PagingParameters pagingParameters)
    {
        var result = await _adminService.GetAllModeratorsAsync(pagingParameters);
        return !result.IsNullOrEmpty() ? Ok(result) : BadRequest();
    }

    [HttpGet("get-all-users")]
    public async Task<ActionResult> GetAllUsers(PagingParameters pagingParameters)
    {
        var result = await _adminService.GetAllUsers(pagingParameters);
        return !result.IsNullOrEmpty() ? Ok(result) : BadRequest();
    }

    [HttpPost("send-mail-to-user")]
    public async Task<ActionResult> SendEmail([FromBody] EmailMetadata request)
    {
        var result = await _adminService.SendEmailFromStaffAsync(request);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("ban-user")]
    public async Task<ActionResult> BanUser([FromBody] Guid userId)
    {
        var result = await _adminService.BanUserAsync(userId);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpPost("unban-user")]
    public async Task<ActionResult> UnbanUser([FromBody] Guid userId)
    {
        var result = await _adminService.UnbanUserAsync(userId);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
}


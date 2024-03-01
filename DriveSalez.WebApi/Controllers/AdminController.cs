using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            try
            {
                var response = await _adminService.AddColorAsync(color);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-body")]
        public async Task<ActionResult> AddNewBodyType([FromBody] string bodyType)
        {
            try
            {
                var response = await _adminService.AddBodyTypeAsync(bodyType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPost("add-new-currency")]
        public async Task<ActionResult> AddNewCurrency([FromBody] string currencyName)
        {
            try
            {
                var response = await _adminService.AddCurrencyAsync(currencyName);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPost("add-new-drivetrain")]
        public async Task<ActionResult> AddNewDrivetrainType([FromBody] string driveTrainType)
        {
            try
            {
                var response = await _adminService.AddVehicleDrivetrainTypeAsync(driveTrainType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-country")]
        public async Task<ActionResult> AddNewCountryType([FromBody] string country)
        {
            try
            {
                var response = await _adminService.AddCountryAsync(country);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPost("add-new-city")]
        public async Task<ActionResult> AddNewCityType([FromBody] AddNewCityDto request)
        {
            try
            {
                var response = await _adminService.AddCityAsync(request.City, request.CountryId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPost("add-new-gearbox")]
        public async Task<ActionResult> AddNewGearboxType([FromBody] string gearboxType)
        {
            try
            {
                var response = await _adminService.AddVehicleGearboxTypeAsync(gearboxType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-make")]
        public async Task<ActionResult> AddNewMake([FromBody] string make)
        {
            try
            {
                var response = await _adminService.AddMakeAsync(make);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-model")]
        public async Task<ActionResult> AddNewModel([FromBody] AddNewModelDto request)
        {
            try
            {
                var response = await _adminService.AddModelAsync(request.MakeId, request.ModelName);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-fuel")]
        public async Task<ActionResult> AddNewFuelType([FromBody] string fuelType)
        {
            try
            {
                var response = await _adminService.AddVehicleFuelTypeAsync(fuelType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-condition")]
        public async Task<ActionResult> AddNewVehicleCondition([FromBody] AddNewConditionDto request)
        {
            try
            {
                var response = await _adminService.AddVehicleConditionAsync(request.Condition, request.Description);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-market-version")]
        public async Task<ActionResult> AddNewVehicleMarketVersion([FromBody] string marketVersion)
        {
            try
            {
                var response = await _adminService.AddVehicleMarketVersionAsync(marketVersion);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-new-option")]
        public async Task<ActionResult> AddNewVehicleOption([FromBody] string option)
        {
            try
            {
                var response = await _adminService.AddVehicleOptionAsync(option);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("update-color")]
        public async Task<ActionResult> UpdateVehicleColor([FromBody] UpdateColorDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleColorAsync(request.ColorId, request.NewColor);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-body")]
        public async Task<ActionResult> UpdateVehicleBodyType([FromBody] UpdateBodyTypeDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleBodyTypeAsync(request.BodyTypeId, request.NewBodyType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-account-limit")]
        public async Task<ActionResult> UpdateUserLimit([FromBody] UpdateUserLimitDto request)
        {
            try
            {
                var response = await _adminService.UpdateAccountLimitAsync(request.LimitId, request.PremiumLimit, request.RegularLimit);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-currency")]
        public async Task<ActionResult> UpdateCurrency([FromBody] UpdateCurrencyDto request)
        {
            try
            {
                var response = await _adminService.UpdateCurrencyAsync(request.CurrencyId, request.NewCurrencyName);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-subscription")]
        public async Task<ActionResult> UpdateSubscription([FromBody] UpdateSubscriptionDto request)
        {
            try
            {
                var response = await _adminService.UpdateSubscriptionAsync(request.SubscriptionId,
                    request.Price, request.CurrencyId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-city")]
        public async Task<ActionResult> UpdateCity([FromBody] UpdateCityDto request)
        {
            try
            {
                var response = await _adminService.UpdateCityAsync(request.CityId, request.NewCity);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-country")]
        public async Task<ActionResult> UpdateCountry([FromBody] UpdateCoutryDto request)
        {
            try
            {
                var response = await _adminService.UpdateCountryAsync(request.CountryId, request.NewCountry);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-drivetrain")]
        public async Task<ActionResult> UpdateVehicleDriveTrainType([FromBody] UpdateDrivetrainDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleDrivetrainTypeAsync(request.DrivetrainId, request.NewDrivetrain);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-gearbox")]
        public async Task<ActionResult> UpdateVehicleGearboxType([FromBody] UpdateGearboxDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleGearboxTypeAsync(request.GearboxId, request.NewGearbox);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-make")]
        public async Task<ActionResult> UpdateMake([FromBody] UpdateMakeDto request)
        {
            try
            {
                var response = await _adminService.UpdateMakeAsync(request.MakeId, request.NewMake);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-model")]
        public async Task<ActionResult> UpdateModel([FromBody] UpdateModelDto request)
        {
            try
            {
                var response = await _adminService.UpdateModelAsync(request.ModelId, request.NewModel);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-fuel")]
        public async Task<ActionResult> UpdateFuelType([FromBody] UpdateFuelTypeDto request)
        {
            try
            {
                var response = await _adminService.UpdateFuelTypeAsync(request.FuelTypeId, request.NewFuelType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            } 
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-condition")]
        public async Task<ActionResult> UpdateVehicleCondition([FromBody] UpdateVehicleConditionDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleConditionAsync(request.VehicleConditionId, request.NewVehicleCondition, request.NewDescription);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-option")]
        public async Task<ActionResult> UpdateVehicleOption([FromBody] UpdateVehicleOptionDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleOptionAsync(request.VehicleOptionId, request.NewVehicleOption);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPut("update-market-version")]
        public async Task<ActionResult> UpdateVehicleMarketVersion([FromBody] UpdateMarketVersionDto request)
        {
            try
            {
                var response = await _adminService.UpdateVehicleMarketVersionAsync(request.MarketVersionId, request.NewMarketVersion);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-color")]
        public async Task<ActionResult> DeleteVehicleColor([FromBody] int colorId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleColorAsync(colorId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpDelete("delete-currency")]
        public async Task<ActionResult> DeleteCurrency([FromBody] int currencyId)
        {
            try
            {
                var response = await _adminService.DeleteCurrencyAsync(currencyId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-body")]
        public async Task<ActionResult> DeleteVehicleBodyType([FromBody] int bodyTypeId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleBodyTypeAsync(bodyTypeId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-country")]
        public async Task<ActionResult> DeleteCountry([FromBody] int countryId)
        {
            try
            {
                var response = await _adminService.DeleteCountryAsync(countryId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-city")]
        public async Task<ActionResult> DeleteCity([FromBody] int cityId)
        {
            try
            {
                var response = await _adminService.DeleteCityAsync(cityId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-drivetrain")]
        public async Task<ActionResult> DeleteVehicleDrivetrainType([FromBody] int drivetrainId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleDrivetrainTypeAsync(drivetrainId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-gearbox")]
        public async Task<ActionResult> DeleteVehicleGearboxType([FromBody] int gearboxId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleGearboxTypeAsync(gearboxId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-make")]
        public async Task<ActionResult> DeleteMake([FromBody] int makeId)
        {
            try
            {
                var response = await _adminService.DeleteMakeAsync(makeId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-model")]
        public async Task<ActionResult> DeleteModel([FromBody] int modelId)
        {
            try
            {
                var response = await _adminService.DeleteModelAsync(modelId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-fuel")]
        public async Task<ActionResult> DeleteFuelType([FromBody] int fuelTypeId)
        {
            try
            {
                var response = await _adminService.DeleteFuelTypeAsync(fuelTypeId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            } 
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-condition")]
        public async Task<ActionResult> DeleteVehicleCondition([FromBody] int vehicleConditionId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleConditionAsync(vehicleConditionId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-option")]
        public async Task<ActionResult> DeleteVehicleOption([FromBody] int vehicleConditionId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleOptionAsync(vehicleConditionId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpDelete("delete-market-version")]
        public async Task<ActionResult> DeleteVehicleMarketVersion([FromBody] int marketVersionId)
        {
            try
            {
                var response = await _adminService.DeleteVehicleMarketVersionAsync(marketVersionId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("create-moderator")]
        public async Task<ActionResult> CreateModerator([FromBody] RegisterModeratorDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var result = await _adminService.AddModeratorAsync(request);
                return result != null ? Ok() : BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("delete-moderator")]
        public async Task<ActionResult> DeleteModerator([FromBody] Guid moderatorId)
        {
            try
            {
                var result = await _adminService.DeleteModeratorAsync(moderatorId);
                return result != null ? Ok(result) : BadRequest(result);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpGet("get-all-moderators")]
        public async Task<ActionResult> GetAllModerators()
        {
            try
            {
                var result = await _adminService.GetAllModeratorsAsync();
                return !result.IsNullOrEmpty() ? Ok(result) : BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var result = await _adminService.GetAllUsers();
                return !result.IsNullOrEmpty() ? Ok(result) : BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("send-mail-to-user")]
        public async Task<ActionResult> SendEmail([FromBody] SendEmailFromStaffDto request)
        {
            try
            {
                var result = await _adminService.SendEmailFromStaffAsync(request.ToEmail, request.Subject, request.Body);

                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("ban-user")]
        public async Task<ActionResult> BanUser([FromBody] Guid userId)
        {
            try
            {
                var result = await _adminService.BanUserAsync(userId);

                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
        
        [HttpPost("unban-user")]
        public async Task<ActionResult> UnbanUser([FromBody] Guid userId)
        {
            try
            {
                var result = await _adminService.UnbanUserAsync(userId);

                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e);
            }
            catch (InvalidOperationException e)
            {
                return Problem(e.Message);
            }
        }
    }
}

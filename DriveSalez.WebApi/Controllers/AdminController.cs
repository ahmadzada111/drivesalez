using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("add-new-color")]
        public async Task<ActionResult> AddNewColor([FromBody] string color)
        {
            try
            {
                var response = await _adminService.AddColorAsync(color);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
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
        }

        [HttpPost("add-new-subscription")]
        public async Task<ActionResult> AddNewSubscription([FromBody] string subscriptionName, decimal price, int currencyId)
        {
            try
            {
                var response = await _adminService.AddSubscriptionAsync(subscriptionName, price, currencyId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
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
        }

        [HttpPost("add-new-condition")]
        public async Task<ActionResult> AddNewVehicleCondition([FromBody] string condition)
        {
            try
            {
                var response = await _adminService.AddVehicleConditionAsync(condition);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
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
        }

        [HttpPut("update-color/{colorId}")]
        public async Task<ActionResult> UpdateVehicleColor([FromRoute] int colorId, [FromBody] string newColor)
        {
            try
            {
                var response = await _adminService.UpdateVehicleColorAsync(colorId, newColor);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-body/{bodyTypeId}")]
        public async Task<ActionResult> UpdateVehicleBodyType([FromRoute] int bodyTypeId, [FromBody] string newBodyType)
        {
            try
            {
                var response = await _adminService.UpdateVehicleBodyTypeAsync(bodyTypeId, newBodyType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-account-limit/{limitId}")]
        public async Task<ActionResult> UpdateVehicleBodyType([FromRoute] int limitId, [FromBody] int limit)
        {
            try
            {
                var response = await _adminService.UpdateAccountLimitAsync(limitId, limit);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-currency/{currencyId}")]
        public async Task<ActionResult> UpdateCurrency([FromRoute] int currencyId, [FromBody] string currencyName )
        {
            try
            {
                var response = await _adminService.UpdateCurrencyAsync(currencyId, currencyName);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-subscription/{subscriptionId}")]
        public async Task<ActionResult> UpdateCurrency([FromRoute] int subscriptionId, [FromBody] string subscriptionName, 
            decimal price, 
            int currencyId)
        {
            try
            {
                var response = await _adminService.UpdateSubscriptionAsync(subscriptionId, subscriptionName, price, currencyId);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-drivetrain/{drivetrainId}")]
        public async Task<ActionResult> UpdateVehicleDriveTrainType([FromRoute] int drivetrainId, [FromBody] string drivetrain)
        {
            try
            {
                var response = await _adminService.UpdateVehicleDrivetrainTypeAsync(drivetrainId, drivetrain);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-gearbox/{gearboxId}")]
        public async Task<ActionResult> UpdateVehicleGearboxType([FromRoute] int gearboxId, [FromBody] string newGearbox)
        {
            try
            {
                var response = await _adminService.UpdateVehicleGearboxTypeAsync(gearboxId, newGearbox);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-make/{makeId}")]
        public async Task<ActionResult> UpdateMake([FromRoute] int makeId, [FromBody] string newMake)
        {
            try
            {
                var response = await _adminService.UpdateMakeAsync(makeId, newMake);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-model/{modelId}")]
        public async Task<ActionResult> UpdateModel([FromRoute] int modelId, [FromBody] string newModel)
        {
            try
            {
                var response = await _adminService.UpdateModelAsync(modelId, newModel);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-fuel/{fuelTypeId}")]
        public async Task<ActionResult> UpdateFuelType([FromRoute] int fuelTypeId, [FromBody] string newFuelType)
        {
            try
            {
                var response = await _adminService.UpdateFuelTypeAsync(fuelTypeId, newFuelType);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            } 
        }
        
        [HttpPut("update-condition/{vehicleConditionId}")]
        public async Task<ActionResult> UpdateVehicleCondition([FromRoute] int vehicleConditionId, [FromBody] string newVehicleCondition)
        {
            try
            {
                var response = await _adminService.UpdateVehicleConditionAsync(vehicleConditionId, newVehicleCondition);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-option/{vehicleOptionId}")]
        public async Task<ActionResult> UpdateVehicleOption([FromRoute] int vehicleConditionId, [FromBody] string newVehicleOption)
        {
            try
            {
                var response = await _adminService.UpdateVehicleOptionAsync(vehicleConditionId, newVehicleOption);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPut("update-market-version/{marketVersionId}")]
        public async Task<ActionResult> UpdateVehicleMarketVersion([FromRoute] int marketVersionId, [FromBody] string newMarketVersion)
        {
            try
            {
                var response = await _adminService.UpdateVehicleMarketVersionAsync(marketVersionId, newMarketVersion);
                return response != null ? Ok(response) : BadRequest(response);
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpDelete("delete-color/{colorId}")]
        public async Task<ActionResult> DeleteVehicleColor([FromRoute] int colorId)
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
        
        [HttpDelete("delete-currency/{currencyId}")]
        public async Task<ActionResult> DeleteCurrency([FromRoute] int currencyId)
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
        }
        
        [HttpDelete("delete-body/{bodyTypeId}")]
        public async Task<ActionResult> DeleteVehicleBodyType([FromRoute] int bodyTypeId)
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
        }
        
        [HttpDelete("delete-drivetrain/{drivetrainId}")]
        public async Task<ActionResult> DeleteVehicleDriveTrainType([FromRoute] int drivetrainId)
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
        }
        
        [HttpDelete("delete-gearbox/{gearboxId}")]
        public async Task<ActionResult> DeleteVehicleGearboxType([FromRoute] int gearboxId)
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
        }
        
        [HttpDelete("delete-make/{makeId}")]
        public async Task<ActionResult> DeleteMake([FromRoute] int makeId)
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
        }
        
        [HttpDelete("delete-model/{modelId}")]
        public async Task<ActionResult> DeleteModel([FromRoute] int modelId)
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
        }
        
        [HttpDelete("delete-fuel/{fuelTypeId}")]
        public async Task<ActionResult> DeleteFuelType([FromRoute] int fuelTypeId)
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
        }
        
        [HttpDelete("delete-condition/{vehicleConditionId}")]
        public async Task<ActionResult> DeleteVehicleCondition([FromRoute] int vehicleConditionId)
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
        }
        
        [HttpDelete("delete-option/{vehicleOptionId}")]
        public async Task<ActionResult> DeleteVehicleOption([FromRoute] int vehicleConditionId)
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
        }
        
        [HttpDelete("delete-market-version/{marketVersionId}")]
        public async Task<ActionResult> DeleteVehicleMarketVersion([FromRoute] int marketVersionId)
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
        }

        [HttpPost("create-moderator")]
        public async Task<ActionResult<ApplicationUser>> CreateModerator([FromBody] RegisterModeratorDto request)
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
        }
    }
}

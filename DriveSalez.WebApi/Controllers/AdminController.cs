using DriveSalez.Core.DTO;
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
        public async Task<ActionResult> AddNewColor(string color)
        {
            var response = await _adminService.AddColor(color);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-body")]
        public async Task<ActionResult> AddNewBodyType(string bodyType)
        {
            var response = await _adminService.AddBodyType(bodyType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-drivetrain")]
        public async Task<ActionResult> AddNewDrivetrainType(string driveTrainType)
        {
            var response = await _adminService.AddVehicleDrivetrainType(driveTrainType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-gearbox")]
        public async Task<ActionResult> AddNewGearboxType(string gearboxType)
        {
            var response = await _adminService.AddVehicleGearboxType(gearboxType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-make")]
        public async Task<ActionResult> AddNewMake(string make)
        {
            var response = await _adminService.AddMake(make);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-model")]
        public async Task<ActionResult> AddNewModel(int makeId, string model)
        {
            var response = await _adminService.AddModel(makeId, model);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-fuel")]
        public async Task<ActionResult> AddNewFuelType(string fuelType)
        {
            var response = await _adminService.AddVehicleFuelType(fuelType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-condition")]
        public async Task<ActionResult> AddNewVehicleCondition(string condition)
        {
            var response = await _adminService.AddVehicleCondition(condition);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-market-version")]
        public async Task<ActionResult> AddNewVehicleMarketVersion(string marketVersion)
        {
            var response = await _adminService.AddVehicleMarketVersion(marketVersion);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-option")]
        public async Task<ActionResult> AddNewVehicleOption(string option)
        {
            var response = await _adminService.AddVehicleOption(option);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPut("update-color/{colorId}")]
        public async Task<ActionResult> UpdateVehicleColor([FromRoute] int colorId, [FromBody] string newColor)
        {
            var response = await _adminService.UpdateVehicleColor(colorId, newColor);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpPut("update-body/{bodyTypeId}")]
        public async Task<ActionResult> UpdateVehicleBodyType([FromRoute] int bodyTypeId, [FromBody] string newBodyType)
        {
            var response = await _adminService.UpdateVehicleBodyType(bodyTypeId, newBodyType);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpPut("update-drivetrain/{drivetrainId}")]
        public async Task<ActionResult> UpdateVehicleDriveTrainType([FromRoute] int drivetrainId, [FromBody] string drivetrain)
        {
            var response = await _adminService.UpdateVehicleDrivetrainType(drivetrainId, drivetrain);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpPut("update-gearbox/{gearboxId}")]
        public async Task<ActionResult> UpdateVehicleGearboxType([FromRoute] int gearboxId, [FromBody] string newGearbox)
        {
            var response = await _adminService.UpdateVehicleGearboxType(gearboxId, newGearbox);
            return response != null ? Ok(response) : BadRequest(response);   
        }
        
        [HttpPut("update-make/{makeId}")]
        public async Task<ActionResult> UpdateMake([FromRoute] int makeId, [FromBody] string newMake)
        {
            var response = await _adminService.UpdateMake(makeId, newMake);
            return response != null ? Ok(response) : BadRequest(response);   
        }
        
        [HttpPut("update-model/{modelId}")]
        public async Task<ActionResult> UpdateModel([FromRoute] int modelId, [FromBody] string newModel)
        {
            var response = await _adminService.UpdateModel(modelId, newModel);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpPut("update-fuel/{fuelTypeId}")]
        public async Task<ActionResult> UpdateFuelType([FromRoute] int fuelTypeId, [FromBody] string newFuelType)
        {
            var response = await _adminService.UpdateFuelType(fuelTypeId, newFuelType);
            return response != null ? Ok(response) : BadRequest(response);   
        }
        
        [HttpPut("update-condition/{vehicleConditionId}")]
        public async Task<ActionResult> UpdateVehicleCondition([FromRoute] int vehicleConditionId, [FromBody] string newVehicleCondition)
        {
            var response = await _adminService.UpdateVehicleCondition(vehicleConditionId, newVehicleCondition);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpPut("update-option/{vehicleOptionId}")]
        public async Task<ActionResult> UpdateVehicleOption([FromRoute] int vehicleConditionId, [FromBody] string newVehicleOption)
        {
            var response = await _adminService.UpdateVehicleOption(vehicleConditionId, newVehicleOption);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpPut("update-market-version/{marketVersionId}")]
        public async Task<ActionResult> UpdateVehicleMarketVersion([FromRoute] int marketVersionId, [FromBody] string newMarketVersion)
        {
            var response = await _adminService.UpdateVehicleMarketVersion(marketVersionId, newMarketVersion);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpDelete("delete-color/{colorId}")]
        public async Task<ActionResult> DeleteVehicleColor([FromRoute] int colorId)
        {
            var response = await _adminService.DeleteVehicleColor(colorId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpDelete("delete-body/{bodyTypeId}")]
        public async Task<ActionResult> DeleteVehicleBodyType([FromRoute] int bodyTypeId)
        {
            var response = await _adminService.DeleteVehicleBodyType(bodyTypeId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpDelete("delete-drivetrain/{drivetrainId}")]
        public async Task<ActionResult> DeleteVehicleDriveTrainType([FromRoute] int drivetrainId)
        {
            var response = await _adminService.DeleteVehicleDrivetrainType(drivetrainId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpDelete("delete-gearbox/{gearboxId}")]
        public async Task<ActionResult> DeleteVehicleGearboxType([FromRoute] int gearboxId)
        {
            var response = await _adminService.DeleteVehicleGearboxType(gearboxId);
            return response != null ? Ok(response) : BadRequest(response);   
        }
        
        [HttpDelete("delete-make/{makeId}")]
        public async Task<ActionResult> DeleteMake([FromRoute] int makeId)
        {
            var response = await _adminService.DeleteMake(makeId);
            return response != null ? Ok(response) : BadRequest(response);   
        }
        
        [HttpDelete("delete-model/{modelId}")]
        public async Task<ActionResult> DeleteModel([FromRoute] int modelId)
        {
            var response = await _adminService.DeleteModel(modelId);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpDelete("delete-fuel/{fuelTypeId}")]
        public async Task<ActionResult> DeleteFuelType([FromRoute] int fuelTypeId)
        {
            var response = await _adminService.DeleteFuelType(fuelTypeId);
            return response != null ? Ok(response) : BadRequest(response);   
        }
        
        [HttpDelete("delete-condition/{vehicleConditionId}")]
        public async Task<ActionResult> DeleteVehicleCondition([FromRoute] int vehicleConditionId)
        {
            var response = await _adminService.DeleteVehicleCondition(vehicleConditionId);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpDelete("delete-option/{vehicleOptionId}")]
        public async Task<ActionResult> DeleteVehicleOption([FromRoute] int vehicleConditionId)
        {
            var response = await _adminService.DeleteVehicleOption(vehicleConditionId);
            return response != null ? Ok(response) : BadRequest(response);  
        }
        
        [HttpDelete("delete-market-version/{marketVersionId}")]
        public async Task<ActionResult> DeleteVehicleMarketVersion([FromRoute] int marketVersionId)
        {
            var response = await _adminService.DeleteVehicleMarketVersion(marketVersionId);
            return response != null ? Ok(response) : BadRequest(response);  
        }

        [HttpPost("create-moderator")]
        public async Task<ActionResult<ApplicationUser>> CreateModerator(RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var result = await _adminService.AddModerator(request);
            return result != null ? Ok() : BadRequest();
        }
    }
}

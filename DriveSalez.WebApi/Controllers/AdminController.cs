using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost("AddNewColor")]
        public ActionResult AddNewColor(string color)
        {
            var response = _adminService.AddColor(color);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewBodyType")]
        public ActionResult AddNewBodyType(string bodyType)
        {
            var response = _adminService.AddBodyType(bodyType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewDriveTrainType")]
        public ActionResult AddNewDriveTrainType(string driveTrainType)
        {
            var response = _adminService.AddVehicleDriveTrainType(driveTrainType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewGearboxType")]
        public ActionResult AddNewGearboxType(string gearboxType)
        {
            var response = _adminService.AddVehicleGearboxType(gearboxType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewMake")]
        public ActionResult AddNewMake(string make)
        {
            var response = _adminService.AddMake(make);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewModel")]
        public ActionResult AddNewModel(int makeId, string model)
        {
            var response = _adminService.AddModel(makeId, model);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewFuelType")]
        public ActionResult AddNewFuelType(string fuelType)
        {
            var response = _adminService.AddVehicleFuelType(fuelType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewVehicleDetailsCondition")]
        public ActionResult AddNewVehicleDetailsCondition(string condition)
        {
            var response = _adminService.AddVehicleDetailsCondition(condition);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewVehicleMarketVersion")]
        public ActionResult AddNewVehicleMarketVersion(string marketVersion)
        {
            var response = _adminService.AddVehicleMarketVersion(marketVersion);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("AddNewVehicleDetailsOption")]
        public ActionResult AddNewVehicleDetailsOption(string option)
        {
            var response = _adminService.AddVehicleDetailsOption(option);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllColors")]
        public ActionResult GetAllColors()
        {
            var response = _adminService.GetAllColors();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllBodyTypes")]
        public ActionResult GetAllBodyTypes()
        {
            var response = _adminService.GetAllVehicleBodyTypes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllDriveTrainTypes")]
        public ActionResult GetAllDriveTrainTypes()
        {
            var response = _adminService.GetAllVehicleDriveTrains();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllGearboxTypes")]
        public ActionResult GetAllGearboxTypes()
        {
            var response = _adminService.GetAllVehicleGearboxTypes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllMakes")]
        public ActionResult GetAllMakes()
        {
            var response = _adminService.GetAllMakes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllModels")]
        public ActionResult GetAllModels(int id)
        {
            var response = _adminService.GetAllModelsByMakeId(id);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetFuelTypes")]
        public ActionResult GetFuelTypes()
        {
            var response = _adminService.GetAllVehicleFuelTypes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetVehicleDetailsConditions")]
        public ActionResult GetVehicleDetailsConditions()
        {
            var response = _adminService.GetAllVehicleDetailsConditions();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetVehicleMarketVersions")]
        public ActionResult GetVehicleMarketVersions()
        {
            var response = _adminService.GetAllVehicleMarketVersions();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetVehicleDetailsOptions")]
        public ActionResult GetVehicleDetailsOptions()
        {
            var response = _adminService.GetAllVehicleDetailsOptions();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("CreateModerator")]
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

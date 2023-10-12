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

        [HttpPost("add-new-color")]
        public ActionResult AddNewColor(string color)
        {
            var response = _adminService.AddColor(color);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-body-type")]
        public ActionResult AddNewBodyType(string bodyType)
        {
            var response = _adminService.AddBodyType(bodyType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-drive-train-type")]
        public ActionResult AddNewDriveTrainType(string driveTrainType)
        {
            var response = _adminService.AddVehicleDriveTrainType(driveTrainType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-gearbox-type")]
        public ActionResult AddNewGearboxType(string gearboxType)
        {
            var response = _adminService.AddVehicleGearboxType(gearboxType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-make")]
        public ActionResult AddNewMake(string make)
        {
            var response = _adminService.AddMake(make);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-model")]
        public ActionResult AddNewModel(int makeId, string model)
        {
            var response = _adminService.AddModel(makeId, model);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-fuel-type")]
        public ActionResult AddNewFuelType(string fuelType)
        {
            var response = _adminService.AddVehicleFuelType(fuelType);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-vehicle-details-condition")]
        public ActionResult AddNewVehicleDetailsCondition(string condition)
        {
            var response = _adminService.AddVehicleDetailsCondition(condition);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-vehicle-market-version")]
        public ActionResult AddNewVehicleMarketVersion(string marketVersion)
        {
            var response = _adminService.AddVehicleMarketVersion(marketVersion);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-new-vehicle-details-option")]
        public ActionResult AddNewVehicleDetailsOption(string option)
        {
            var response = _adminService.AddVehicleDetailsOption(option);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-colors")]
        public ActionResult GetAllColors()
        {
            var response = _adminService.GetAllColors();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-body-types")]
        public ActionResult GetAllBodyTypes()
        {
            var response = _adminService.GetAllVehicleBodyTypes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-drive-train-types")]
        public ActionResult GetAllDriveTrainTypes()
        {
            var response = _adminService.GetAllVehicleDriveTrains();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-gearbox-types")]
        public ActionResult GetAllGearboxTypes()
        {
            var response = _adminService.GetAllVehicleGearboxTypes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-makes")]
        public ActionResult GetAllMakes()
        {
            var response = _adminService.GetAllMakes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-models")]
        public ActionResult GetAllModels(int id)
        {
            var response = _adminService.GetAllModelsByMakeId(id);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-fuel-types")]
        public ActionResult GetAllFuelTypes()
        {
            var response = _adminService.GetAllVehicleFuelTypes();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-vehicle-details-conditions")]
        public ActionResult GetAllVehicleDetailsConditions()
        {
            var response = _adminService.GetAllVehicleDetailsConditions();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-vehicle-market-versions")]
        public ActionResult GetAllVehicleMarketVersions()
        {
            var response = _adminService.GetAllVehicleMarketVersions();
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-vehicle-details-options")]
        public ActionResult GetAllVehicleDetailsOptions()
        {
            var response = _adminService.GetAllVehicleDetailsOptions();
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

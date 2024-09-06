// using Asp.Versioning;
// using DriveSalez.Application.Contracts.ServiceContracts;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
//
// namespace DriveSalez.Presentation.Controllers;
//
// /// <summary>
// /// Handles requests related to vehicle details such as colors, body types, and makes.
// /// </summary>
// [ApiController]
// [ApiVersion(1)]
// [Route("api/v{v:apiVersion}/details")]
// [AllowAnonymous]
// public class DetailsController : Controller
// {
//     private readonly IDetailsService _detailsService;
//     private readonly ILogger _logger;
//     
//     /// <summary>
//     /// Initializes a new instance of the <see cref="DetailsController"/> class.
//     /// </summary>
//     /// <param name="detailsService">Service to handle requests related to vehicle details.</param>
//     /// <param name="logger">Logger for the controller.</param>
//     public DetailsController(IDetailsService detailsService, ILogger<DetailsController> logger)
//     {
//         _detailsService = detailsService;
//         _logger = logger;
//     }
//
//     /// <summary>
//     /// Retrieves a list of all vehicle colors.
//     /// </summary>
//     /// <returns>List of all vehicle colors.</returns>
//     [HttpGet("colors")]
//     public async Task<ActionResult> GetAllColors()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//         
//         var response = await _detailsService.GetAllColorsAsync();
//         return Ok(response);
//     }
//
//     /// <summary>
//     /// Retrieves a list of all vehicle body types.
//     /// </summary>
//     /// <returns>List of all vehicle body types.</returns>
//     [HttpGet("body-types")]
//     public async Task<ActionResult> GetAllBodyTypes()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleBodyTypesAsync();
//         return Ok(response);
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all drivetrain types for vehicles.
//     /// </summary>
//     /// <returns>List of all vehicle drivetrain types.</returns>
//     [HttpGet("drivetrain-types")]
//     public async Task<ActionResult> GetAllDrivetrainTypes()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleDrivetrainsAsync();
//         return Ok(response);
//     }
//
//     /// <summary>
//     /// Retrieves a list of all gearbox types for vehicles.
//     /// </summary>
//     /// <returns>List of all vehicle gearbox types.</returns>
//     [HttpGet("gearbox-types")]
//     public async Task<ActionResult> GetAllGearboxTypes()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleGearboxTypesAsync();
//         return Ok(response);
//     }
//
//     /// <summary>
//     /// Retrieves a list of all vehicle makes.
//     /// </summary>
//     /// <returns>List of all vehicle makes.</returns>
//     [HttpGet("makes")]
//     public async Task<ActionResult> GetAllMakes()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllMakesAsync();
//         return Ok(response);  
//     }
//
//     /// <summary>
//     /// Retrieves a list of all vehicle models.
//     /// </summary>
//     /// <returns>List of all vehicle models.</returns>
//     [HttpGet("models")]
//     public async Task<ActionResult> GetAllModels()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllModelsAsync();
//         return Ok(response);
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all models for a given make.
//     /// </summary>
//     /// <param name="modelId">ID of the vehicle make.</param>
//     /// <returns>List of models for the specified make.</returns>
//     [HttpGet("makes/{modelId}/models")]
//     public async Task<ActionResult> GetAllModelsByMake([FromQuery] int modelId)
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllModelsByMakeIdAsync(modelId);
//         return Ok(response);   
//     }
//
//     /// <summary>
//     /// Retrieves a list of all fuel types for vehicles.
//     /// </summary>
//     /// <returns>List of all vehicle fuel types.</returns>
//     [HttpGet("fuel-types")]
//     public async Task<ActionResult> GetAllFuelTypes()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleFuelTypesAsync();
//         return Ok(response);  
//     }
//
//     /// <summary>
//     /// Retrieves a list of all conditions for vehicles.
//     /// </summary>
//     /// <returns>List of all vehicle conditions.</returns>
//     [HttpGet("conditions")]
//     public async Task<ActionResult> GetAllVehicleConditions()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleDetailsConditionsAsync();
//         return Ok(response);  
//     }
//
//     /// <summary>
//     /// Retrieves a list of all market versions for vehicles.
//     /// </summary>
//     /// <returns>List of all vehicle market versions.</returns>
//     [HttpGet("market-versions")]
//     public async Task<ActionResult> GetAllVehicleMarketVersions()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleMarketVersionsAsync();
//         return Ok(response);  
//     }
//
//     /// <summary>
//     /// Retrieves a list of all options available for vehicles.
//     /// </summary>
//     /// <returns>List of all vehicle options.</returns>
//     [HttpGet("options")]
//     public async Task<ActionResult> GetAllVehicleOptions()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllVehicleDetailsOptionsAsync();
//         return Ok(response);
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all manufacture years for vehicles.
//     /// </summary>
//     /// <returns>List of all manufacture years.</returns>
//     [HttpGet("manufacture-years")]
//     public async Task<ActionResult> GetAllManufactureYears()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllManufactureYearsAsync();
//         return Ok(response);
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all countries.
//     /// </summary>
//     /// <returns>List of all countries.</returns>
//     [HttpGet("countries")]
//     public async Task<ActionResult> GetAllCountries()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllCountriesAsync();
//         return Ok(response);
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all cities.
//     /// </summary>
//     /// <returns>List of all cities.</returns>
//     [HttpGet("cities")]
//     public async Task<ActionResult> GetAllCities()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllCitiesAsync();
//         return Ok(response);
//     }
//
//     /// <summary>
//     /// Retrieves a list of all subscriptions.
//     /// </summary>
//     /// <returns>List of all subscriptions.</returns>
//     [HttpGet("subscriptions")]
//     public async Task<ActionResult> GetAllSubscriptions()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllSubscriptionsAsync();
//         return Ok(response);   
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all announcement pricings.
//     /// </summary>
//     /// <returns>List of all announcement pricings.</returns>
//     [HttpGet("announcement-pricings")]
//     public async Task<ActionResult> GetAllAnnouncementPricings()
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllAnnouncementPricingsAsync();
//         return Ok(response);
//     }
//     
//     /// <summary>
//     /// Retrieves a list of all cities in a specific country.
//     /// </summary>
//     /// <param name="countryId">ID of the country.</param>
//     /// <returns>List of cities in the specified country.</returns>
//     [HttpGet("countries/{countryId}/cities")]
//     public async Task<ActionResult> GetAllCitiesByCountryId([FromQuery] int countryId)
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//
//         var response = await _detailsService.GetAllCitiesByCountryIdAsync(countryId);
//         return Ok(response);
//     }
// }
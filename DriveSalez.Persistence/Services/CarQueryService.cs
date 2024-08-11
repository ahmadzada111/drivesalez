using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleParts;
using Newtonsoft.Json.Linq;

namespace DriveSalez.Persistence.Services;

public class CarQueryService : ICarQueryService
{
    private readonly HttpClient _httpClient;

    public CarQueryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Make>> GetAllMakesAsync()
    {
        var response = await _httpClient.GetAsync("https://www.carqueryapi.com/api/0.3/?callback=?&cmd=getTrims");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var json = StripJsonpPadding(content);
            var jsonData = JObject.Parse(json);
            var trims = jsonData["Trims"];

            var makes = trims
                .Select(t => t["model_make_id"].ToString())
                .Distinct()
                .Select(m => new Make
                {
                    Title = m
                }).ToList();

            return makes;
        }

        return Enumerable.Empty<Make>();
    }

    public async Task<IEnumerable<Model>> GetModelsByMakeAsync(string makeName)
    {
        var response = await _httpClient.GetAsync($"https://www.carqueryapi.com/api/0.3/?cmd=getModels&make={makeName}");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jsonData = JObject.Parse(content);
            var models = jsonData["Models"]
            .Select(m => new Model
            {
                Title = m["model_name"].ToString()
            }).ToList();

            return models;
        }

        return Enumerable.Empty<Model>();
    }

    public async Task<IEnumerable<BodyType>> GetBodyTypesAsync()
    {
        var response = await _httpClient.GetAsync("https://www.carqueryapi.com/api/0.3/?cmd=getTrims");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jsonData = JObject.Parse(content);
            var trims = jsonData["Trims"];
            
            var bodyTypes = trims
                .Select(t => t["model_body"].ToString())
                .Distinct()
                .Select(bt => new BodyType
                {
                    Type = bt
                })
                .ToList();

            return bodyTypes;
        }

        return Enumerable.Empty<BodyType>();
    }

    public async Task<IEnumerable<FuelType>> GetFuelTypesAsync()
    {
        var response = await _httpClient.GetAsync("https://www.carqueryapi.com/api/0.3/?cmd=getTrims");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jsonData = JObject.Parse(content);
            var trims = jsonData["Trims"];
            
            var fuelTypes = trims
                .Select(t => t["model_engine_fuel"].ToString())
                .Distinct()
                .Select(ft => new FuelType
                {
                    Type = ft
                })
                .ToList();

            return fuelTypes;
        }

        return Enumerable.Empty<FuelType>();
    }

    public async Task<IEnumerable<DrivetrainType>> GetDrivetrainTypesAsync()
    {
         var response = await _httpClient.GetAsync("https://www.carqueryapi.com/api/0.3/?cmd=getTrims");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jsonData = JObject.Parse(content);
            var trims = jsonData["Trims"];
            
            var drivetrainTypes = trims
                .Select(t => t["model_drive"].ToString())
                .Distinct()
                .Select(ft => new DrivetrainType
                {
                    Type = ft
                })
                .ToList();

            return drivetrainTypes;
        }

        return Enumerable.Empty<DrivetrainType>();
    }

    private string StripJsonpPadding(string jsonp)
    {
        int startIndex = jsonp.IndexOf('(') + 1;
        int endIndex = jsonp.LastIndexOf(')');
        return jsonp.Substring(startIndex, endIndex - startIndex);
    }
}

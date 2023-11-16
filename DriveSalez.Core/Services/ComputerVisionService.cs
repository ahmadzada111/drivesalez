using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using ImageUrl = DriveSalez.Core.Entities.ImageUrl;

namespace DriveSalez.Core.Services;

public class ComputerVisionService : IComputerVisionService
{
    private readonly IConfiguration _configuration;
    private readonly IComputerVisionClient _computerVisionClient;

    public ComputerVisionService(IConfiguration configuration)
    {
        _configuration = configuration;

        var subscriptionKey = _configuration["ImageAnalyzer:Key"];
        var endpoint = _configuration["ImageAnalyzer:Endpoint"];

        _computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
        {
            Endpoint = endpoint
        };
    }
    
    public async Task<bool> AnalyzeImagesAsync(List<ImageUrl> imageUrls)
    {
        foreach (var imageUrl in imageUrls)
        {
            var analysisResult = await _computerVisionClient.AnalyzeImageAsync(
                imageUrl.Url.ToString(),
                new List<VisualFeatureTypes?>
                {
                    VisualFeatureTypes.Adult,   
                    VisualFeatureTypes.Objects, 
                });

            if (!IsSafeImage(analysisResult) || !ContainsOnlyCar(analysisResult))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsSafeImage(ImageAnalysis analysisResult)
    {
        return analysisResult.Adult.IsAdultContent == false && analysisResult.Adult.IsRacyContent == false;
    }

    private bool ContainsOnlyCar(ImageAnalysis analysisResult)
    {
        return analysisResult.Objects.Any(obj => obj.ObjectProperty.Equals("car", StringComparison.OrdinalIgnoreCase));
    }
}
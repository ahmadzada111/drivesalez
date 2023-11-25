using DriveSalez.Core.ServiceContracts;
using Google.Cloud.Vision.V1;
using ImageUrl = DriveSalez.Core.Entities.ImageUrl;

namespace DriveSalez.Core.Services;

public class ComputerVisionService : IComputerVisionService
{
    private readonly IImageAnnotatorClientProvider _imageAnnotatorClientProvider;
    
    public ComputerVisionService(IImageAnnotatorClientProvider imageAnnotatorClientProvider)
    {
        _imageAnnotatorClientProvider = imageAnnotatorClientProvider;
    }

    public async Task<bool> AnalyzeImagesAsync(List<ImageUrl> imageUrls)
    {
        var annotatorClient = _imageAnnotatorClientProvider.GetImageAnnotatorClient();

        foreach (var imageUrl in imageUrls)
        {
            var image = Image.FromUri(imageUrl.Url);
            var response = await annotatorClient.DetectLabelsAsync(image);

            if (!IsSafeImage(response) || !ContainsCarComponents(response))
            {
                return false;
            }
        }

        return true;
    }
    
    private bool IsSafeImage(IEnumerable<EntityAnnotation> annotations)
    {
        return !annotations.Any(a => a.Description.Contains("explicit", StringComparison.OrdinalIgnoreCase));
    }

    private bool ContainsCarComponents(IEnumerable<EntityAnnotation> annotations)
    {
        var carComponents = new List<string> { "car", "gear shift", "steering wheel", "speedometer", "car interior" };
        return annotations.Any(a => carComponents.Any(c => a.Description.Equals(c, StringComparison.OrdinalIgnoreCase)));
    }
}
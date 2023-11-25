using ImageUrl = DriveSalez.Core.Entities.ImageUrl;

namespace DriveSalez.Core.ServiceContracts;

public interface IComputerVisionService
{
    Task<bool> AnalyzeImagesAsync(List<ImageUrl> imageUrls);
}
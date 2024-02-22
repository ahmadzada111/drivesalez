

using DriveSalez.Core.Domain.Entities;

namespace DriveSalez.Core.ServiceContracts;

public interface IComputerVisionService
{
    Task<bool> AnalyzeImagesAsync(List<ImageUrl> imageUrls);
}
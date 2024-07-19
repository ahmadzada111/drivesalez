using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.ServiceContracts;

public interface IComputerVisionService
{
    Task<bool> AnalyzeImagesAsync(List<ImageUrl> imageUrls);
}
using Google.Cloud.Vision.V1;

namespace DriveSalez.Application.ServiceContracts;

public interface IImageAnnotatorClientProvider
{
    ImageAnnotatorClient GetImageAnnotatorClient();
}
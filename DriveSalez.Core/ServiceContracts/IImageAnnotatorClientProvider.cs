using Google.Cloud.Vision.V1;

namespace DriveSalez.Core.ServiceContracts;

public interface IImageAnnotatorClientProvider
{
    ImageAnnotatorClient GetImageAnnotatorClient();
}
using Azure.Storage.Blobs;
using DriveSalez.Core.ServiceContracts;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Configuration;

namespace DriveSalez.Core.Services;

public class ImageAnnotatorClientProvider : IImageAnnotatorClientProvider
{
    private readonly IConfiguration _configuration;

    public ImageAnnotatorClientProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ImageAnnotatorClient GetImageAnnotatorClient()
    {
        if (!File.Exists(_configuration["ImageAnalyzer:Path"]))
        {
            throw new FileNotFoundException("File is not existing");
        }

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
        {
            string credentialsPath = _configuration["ImageAnalyzer:Path"];
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
        }
        
        var credentials = GoogleCredential.FromFile(_configuration["ImageAnalyzer:Path"]);
        var visionClient = ImageAnnotatorClient.Create();

        return visionClient;
    }
}
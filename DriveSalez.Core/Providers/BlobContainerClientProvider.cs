using Azure.Storage.Blobs;
using DriveSalez.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;

namespace DriveSalez.Core.Providers;

public class BlobContainerClientProvider : IBlobContainerClientProvider
{
    private readonly IConfiguration _blobConfiguration;

    public BlobContainerClientProvider(IConfiguration blobConfiguration)
    {
        _blobConfiguration = blobConfiguration;
    }

    public BlobContainerClient GetContainerClient()
    {
        string containerName = _blobConfiguration["BlobStorage:FileStorage"];
        string connectionString = _blobConfiguration["BlobStorage:ConnectionString"];
        
        BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);

        return blobContainerClient;
    }
}
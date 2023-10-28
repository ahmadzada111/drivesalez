using Azure.Storage.Blobs;

namespace DriveSalez.Core.ServiceContracts;

public interface IBlobContainerClientProvider
{
    public BlobContainerClient GetContainerClient();
}
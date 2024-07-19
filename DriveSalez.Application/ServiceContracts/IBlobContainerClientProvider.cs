using Azure.Storage.Blobs;

namespace DriveSalez.Application.ServiceContracts;

public interface IBlobContainerClientProvider
{
    public BlobContainerClient GetContainerClient();
}
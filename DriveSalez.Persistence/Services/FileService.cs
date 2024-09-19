using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.Settings;
using Microsoft.Extensions.Options;

namespace DriveSalez.Persistence.Services
{
    internal sealed class FileService : IFileService
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public FileService(IOptions<BlobStorageSettings> blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings.Value;
        }

        public async Task<List<ImageUrl>> UploadFilesAsync(List<FileUploadData> filesData, User user)
        {
            List<ImageUrl> uploadedUris = new List<ImageUrl>();
        
            BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
            string userBlobName = $"{user.Id}";

            foreach (var fileData in filesData)
            {
                string fileType = fileData.FileType;
                
                var blobName = $"{userBlobName}/image_{Guid.NewGuid()}.{fileType}";
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                var response = await blobClient.UploadAsync(fileData.Stream, overwrite: true);

                if (response.GetRawResponse().Status == 201)
                {
                    uploadedUris.Add(new ImageUrl { Url = blobClient.Uri });
                }
                else
                {
                    throw new InvalidOperationException($"Failed to upload image: {blobName}");
                }
            }
        
            return uploadedUris;
        }
        
        public async Task<List<ImageUrl>> UpdateFilesAsync(List<FileUploadData> filesData, User user)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
            string userBlobName = $"{user.Id}/";

            var blobItems = blobContainerClient.GetBlobsAsync(prefix: userBlobName);

            await foreach (var blobItem in blobItems)
            {
                var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }

            return await UploadFilesAsync(filesData, user);
        }
        
        public async Task<bool> DeleteFileAsync(ImageUrl imageUrl)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);

            var blobClient = blobContainerClient.GetBlobClient(imageUrl.Url.ToString());
            var result = await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            return result.Value;
        }
        
        public async Task<bool> DeleteAllFilesAsync(Guid userId)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
            
            var blobItems = blobContainerClient.GetBlobsAsync(prefix: userId.ToString());

            await foreach (var blobItem in blobItems)
            {
                var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }

            return true;
        }
    }
}

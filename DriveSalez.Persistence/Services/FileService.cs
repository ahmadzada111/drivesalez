using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DriveSalez.Persistence.Services;

public class FileService : IFileService
{
    private readonly BlobStorageSettings _blobStorageSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
     
    public FileService(IOptions<BlobStorageSettings> blobStorageSettings, UserManager<ApplicationUser> userManager, 
        IHttpContextAccessor contextAccessor)
    {
        _blobStorageSettings = blobStorageSettings.Value;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<ImageUrl>> UploadFilesAsync(List<string> base64Images, ApplicationUser user)
    {
        List<ImageUrl> uploadedUris = new List<ImageUrl>();
        
        BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
        string userBlobName = $"{user.Id}";

        foreach (var base64Image in base64Images)
        {
            byte[] imageBytes = RemovePrefixFromBase64(base64Image);
            string fileType = GetImageTypeFromBase64(base64Image);

            using var stream = new MemoryStream(imageBytes);
            stream.Seek(0, SeekOrigin.Begin);

            var blobName = $"{userBlobName}/image_{Guid.NewGuid()}.{fileType}";
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            Response<BlobContentInfo> response = await blobClient.UploadAsync(stream);

            if (response.GetRawResponse().Status == 201)
            {
                uploadedUris.Add(new ImageUrl { Url = blobClient.Uri });
            }
        }
        
        return uploadedUris;
    }

    public async Task<List<ImageUrl>> UpdateFilesAsync(List<string> base64Images, ApplicationUser user)
    {
        BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
        
        BlobClient existTagsBlobClient = blobContainerClient.GetBlobClient(user.Id.ToString());
        await existTagsBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

        return await UploadFilesAsync(base64Images, user);
    }
    
    public async Task<bool> DeleteFileAsync(ImageUrl imageUrl)
    {
        BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
        
        BlobClient existTagsBlobClient = blobContainerClient.GetBlobClient(imageUrl.Url?.ToString());
        var result = await existTagsBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

        return result.Value;
    }
    
    public async Task<bool> DeleteAllFilesAsync(Guid userId)
    {
        BlobContainerClient blobContainerClient = new BlobContainerClient(_blobStorageSettings.ConnectionString, _blobStorageSettings.ContainerName);
        
        BlobClient existTagsBlobClient = blobContainerClient.GetBlobClient(userId.ToString());
        var result = await existTagsBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

        return result.Value;   
    }
    
    private string GetImageTypeFromBase64(string base64String)
    {
        int prefixEndIndex = base64String.IndexOf(';');
        string prefix = base64String.Substring(0, prefixEndIndex);
        string imageType = prefix.Replace("data:image/", "");

        return imageType;
    }
     
    private byte[] RemovePrefixFromBase64(string base64String)
    {
        int prefixEndIndex = base64String.IndexOf(',') + 1;
        string base64WithoutPrefix = base64String.Substring(prefixEndIndex);
        byte[] bytes = Convert.FromBase64String(base64WithoutPrefix);

        return bytes;
    }
}
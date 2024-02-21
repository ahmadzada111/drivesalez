using System.Text;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Google.Apis.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.Services;

public class FileService : IFileService
{
     private readonly IBlobContainerClientProvider _containerClient;
     private readonly UserManager<ApplicationUser> _userManager;
     private readonly IHttpContextAccessor _contextAccessor;
     
     public FileService(IBlobContainerClientProvider containerClient, UserManager<ApplicationUser> userManager, 
          IHttpContextAccessor contextAccessor)
     {
          _containerClient = containerClient;
          _userManager = userManager;
          _contextAccessor = contextAccessor;
     }

    public async Task<List<ImageUrl>> UploadFilesAsync(List<string> base64Images)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        List<ImageUrl> uploadedUris = new List<ImageUrl>();
        
        BlobContainerClient blobContainerClient = _containerClient.GetContainerClient();
        string userBlobName = $"{user.Id}";

        foreach (var base64Image in base64Images)
        {
            byte[] imageBytes = RemovePrefixFromBase64(base64Image);
            string fileType = GetImageTypeFromBase64(base64Image);
                
            using (var stream = new MemoryStream(imageBytes))
            {
                stream.Seek(0, SeekOrigin.Begin);
                    
                var blobName = $"{userBlobName}/image_{Guid.NewGuid()}.{fileType}";
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                Response<BlobContentInfo> response = await blobClient.UploadAsync(stream);

                if (response.GetRawResponse().Status == 201)
                {
                    uploadedUris.Add(new ImageUrl { Url = blobClient.Uri });
                }
            }
        }
        
        return uploadedUris;
    }

    public async Task<List<ImageUrl>> UpdateFilesAsync(List<string> base64Images)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        BlobContainerClient imageContainer = _containerClient.GetContainerClient();
        
        try
        {
            BlobClient existTagsBlobClient = imageContainer.GetBlobClient(user.Id.ToString());
            await existTagsBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            return await UploadFilesAsync(base64Images);
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    public async Task<bool> DeleteFileAsync(ImageUrl imageUrl)
    {
        BlobContainerClient imageContainer = _containerClient.GetContainerClient();
        
        try
        {
            BlobClient existTagsBlobClient = imageContainer.GetBlobClient(imageUrl.Url.ToString());
            var result = await existTagsBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            return result.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    public async Task<bool> DeleteAllFilesAsync(Guid userId)
    {
        BlobContainerClient imageContainer = _containerClient.GetContainerClient();
        
        try
        {
            BlobClient existTagsBlobClient = imageContainer.GetBlobClient(userId.ToString());
            var result = await existTagsBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            return result.Value;
        }
        catch (Exception)
        {
            throw;
        }
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
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DriveSalez.Core.Services;

public class FileService : IFileService
{
     private readonly IBlobContainerClientProvider _containerClient;
     private readonly IConfiguration _blobConfig;
     private readonly UserManager<ApplicationUser> _userManager;
     private readonly IHttpContextAccessor _contextAccessor;
     
     public FileService(IBlobContainerClientProvider containerClient, UserManager<ApplicationUser> userManager, 
          IHttpContextAccessor contextAccessor)
     {
          _containerClient = containerClient;
          _userManager = userManager;
          _contextAccessor = contextAccessor;
     }

     public async Task<List<Uri>> UploadFilesAsync(List<IFormFile> files)
     {
          var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

          if (user == null)
          {
               throw new UserNotAuthorizedException("User is not authorized!");
          }

          List<Uri> uploadedUris = new List<Uri>();

          try
          {
               BlobContainerClient blobContainerClient = _containerClient.GetContainerClient();
               string userBlobName = $"{user.Id}";

               foreach (var file in files)
               {
                    if (file.Length > 0)
                    {
                         using (var stream = new MemoryStream())
                         {
                              await file.CopyToAsync(stream);
                              stream.Seek(0, SeekOrigin.Begin);

                              var blobName = $"{userBlobName}/{file.FileName}";
                              var blobClient = blobContainerClient.GetBlobClient(blobName);

                              Response<BlobContentInfo> response = await blobClient.UploadAsync(stream);
                              
                              if (response.GetRawResponse().Status == 201)
                              {
                                   uploadedUris.Add(blobClient.Uri);
                              }
                         }
                    }
               }
          }
          catch (RequestFailedException)
          {
               throw;
          }

          return uploadedUris;
     }
}
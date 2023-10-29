using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DriveSalez.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;

namespace DriveSalez.Core.Services;

public class FileService : IFileService
{
     private readonly IBlobContainerClientProvider _containerClient;
     private readonly IConfiguration _blobConfig;
     
     public FileService(IBlobContainerClientProvider containerClient, IConfiguration blobConfig)
     {
          _containerClient = containerClient;
          _blobConfig = blobConfig;
     }

     public async Task<Uri> UploadFilesAsync()
     {
          MemoryStream stream = new MemoryStream();
          string filePath = "/Users/ahmad/Desktop/drivesalez/drivesalez/DriveSalez.Core/DTO/CreateAnnouncementDto.cs";

          try
          {
               using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
               {
                    // Copy the file content to the MemoryStream
                    fileStream.CopyTo(stream);
               }

               // Reset the position of the stream to the start
               stream.Seek(0, SeekOrigin.Begin);

               BlobContainerClient blobClient = _containerClient.GetContainerClient();
               string blobName = _blobConfig["BlobStorage:FileStorage"]; // Blob name from configuration
               var blob = blobClient.GetBlobClient(blobName);

               Response<BlobContentInfo> response = await blob.UploadAsync(stream);

               if (response.GetRawResponse().Status == 201) // 201 indicates a successful upload
               {
                    return blob.Uri;
               }
          }
          catch (RequestFailedException ex)
          {
               // Handle exceptions (e.g., container does not exist, invalid credentials, etc.)
               // You can log the exception and handle it as needed.
          }

          return null;
     }

}
using DriveSalez.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DriveSalez.Core.ServiceContracts;

public interface IFileService
{
    Task<List<ImageUrl>> UploadFilesAsync(List<string> files);

    Task<List<ImageUrl>> UpdateFilesAsync(List<string> base64Images);

    Task<bool> DeleteFilesAsync(List<ImageUrl> imageUrls);
    
    Task<bool> DeleteAllFilesAsync(Guid userId);
}
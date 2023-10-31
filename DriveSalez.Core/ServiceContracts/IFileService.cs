using Microsoft.AspNetCore.Http;

namespace DriveSalez.Core.ServiceContracts;

public interface IFileService
{
    Task<List<Uri>> UploadFilesAsync(List<IFormFile> files);
}
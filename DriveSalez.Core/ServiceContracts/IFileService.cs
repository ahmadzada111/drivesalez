using DriveSalez.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace DriveSalez.Core.ServiceContracts;

public interface IFileService
{
    Task<List<ImageUrl>> UploadFilesAsync(List<string> files);
}
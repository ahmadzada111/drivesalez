using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.ServiceContracts;

public interface IFileService
{
    Task<List<ImageUrl>> UploadFilesAsync(List<string> files);

    Task<List<ImageUrl>> UpdateFilesAsync(List<string> base64Images);

    Task<bool> DeleteFileAsync(ImageUrl imageUrl);
    
    Task<bool> DeleteAllFilesAsync(Guid userId);
}
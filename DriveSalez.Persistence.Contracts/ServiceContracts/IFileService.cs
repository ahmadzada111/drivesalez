using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Persistence.Contracts.ServiceContracts;

public interface IFileService
{
    Task<List<ImageUrl>> UploadFilesAsync(List<string> base64Images, UserProfile user);

    Task<List<ImageUrl>> UpdateFilesAsync(List<string> base64Images, UserProfile user);

    Task<bool> DeleteFileAsync(ImageUrl imageUrl);
    
    Task<bool> DeleteAllFilesAsync(Guid userId);
}
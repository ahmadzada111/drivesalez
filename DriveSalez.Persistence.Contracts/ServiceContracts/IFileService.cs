using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.DTO;

namespace DriveSalez.Persistence.Contracts.ServiceContracts;

public interface IFileService
{
    Task<List<ImageUrl>> UploadFilesAsync(List<FileUploadData> filesData, User user);
    
    Task<List<ImageUrl>> UpdateFilesAsync(List<FileUploadData> filesData, User user);

    Task<bool> DeleteFileAsync(ImageUrl imageUrl);
    
    Task<bool> DeleteAllFilesAsync(Guid userId);
}
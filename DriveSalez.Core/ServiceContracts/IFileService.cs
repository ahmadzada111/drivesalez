namespace DriveSalez.Core.ServiceContracts;

public interface IFileService
{
    Task<Uri> UploadFilesAsync();
}
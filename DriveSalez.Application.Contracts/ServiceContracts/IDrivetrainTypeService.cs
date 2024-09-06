using DriveSalez.SharedKernel.DTO.DrivetrainTypeDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IDrivetrainTypeService
{
    Task<DrivetrainTypeDto> CreateDrivetrainType(string type);

    Task<IEnumerable<DrivetrainTypeDto>> GetDrivetrainTypes();

    Task<DrivetrainTypeDto> FindDrivetrainTypeById(int id);

    Task<DrivetrainTypeDto> UpdateDrivetrainType(DrivetrainTypeDto drivetrainTypeDto);
    
    Task<bool> DeleteDrivetrainType(int id);
}
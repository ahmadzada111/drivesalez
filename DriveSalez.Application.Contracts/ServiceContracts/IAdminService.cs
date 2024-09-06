using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using DriveSalez.SharedKernel.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IAdminService
{
    Task<RegisterModeratorResponseDto?> AddModeratorAsync(RegisterModeratorDto registerDto);
        
    Task<PaginatedList<GetModeratorDto>> GetAllModeratorsAsync(PagingParameters pagingParameters);

    Task<GetModeratorDto?> DeleteModeratorAsync(Guid moderatorId);

    Task<PaginatedList<GetUserDto>> GetAllUsers(PagingParameters pagingParameters);
    
    Task<bool> BanUserAsync(Guid userId, TimeSpan duration);
        
    Task<bool> UnbanUserAsync(Guid userId);
}
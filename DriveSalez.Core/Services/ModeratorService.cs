using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.Services;

public class ModeratorService : IModeratorService
{
    private readonly IModeratorRepository _moderatorRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public ModeratorService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, IModeratorRepository moderatorRepository)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _moderatorRepository = moderatorRepository;
    }
    
    public async Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.MakeAnnouncementActiveInDbAsync(user, announcementId);

        return response;
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.MakeAnnouncementInactiveInDbAsync(user, announcementId);

        return response;
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementWaitingAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.MakeAnnouncementWaitingInDbAsync(user, announcementId);

        return response;
    }
}
using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.Services;

public class ModeratorService : IModeratorService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public ModeratorService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, IAnnouncementRepository announcementRepository)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _announcementRepository = announcementRepository;
    }
    
    public async Task<AnnouncementResponseDto> MakeAnnouncementActiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementInactiveInDbAsync(user.Id, announcementId);

        return response;
    }

    public async Task<AnnouncementResponseDto> MakeAnnouncementInactiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementInactiveInDbAsync(user.Id, announcementId);

        return response;
    }

    public async Task<AnnouncementResponseDto> MakeAnnouncementWaitingAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementInactiveInDbAsync(user.Id, announcementId);

        return response;
    }
}
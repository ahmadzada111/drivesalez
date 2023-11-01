using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
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
    
    public async Task<AnnouncementResponseDto> ChangeAnnouncementStateAsync(Guid announcementId, AnnouncementState announcementState)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.ChangeAnnouncementStateInDbAsync(user.Id, announcementId, announcementState);

        return response;
    }
}
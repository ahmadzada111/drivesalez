using AutoMapper;
using DriveSalez.Application.DTO.AnnouncementDTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

public class ModeratorService : IModeratorService
{
    private readonly IModeratorRepository _moderatorRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    
    public ModeratorService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, 
        IModeratorRepository moderatorRepository, IMapper mapper)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _moderatorRepository = moderatorRepository;
        _mapper = mapper;
    }
    
    public async Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        
        var response = await _moderatorRepository.MakeAnnouncementActiveInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        
        var response = await _moderatorRepository.MakeAnnouncementInactiveInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementWaitingAsync(Guid announcementId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        
        var response = await _moderatorRepository.MakeAnnouncementWaitingInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }
}
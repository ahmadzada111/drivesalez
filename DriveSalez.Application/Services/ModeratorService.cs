using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AnnoucementDTO;
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
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.MakeAnnouncementActiveInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.MakeAnnouncementInactiveInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementWaitingAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _moderatorRepository.MakeAnnouncementWaitingInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }
}
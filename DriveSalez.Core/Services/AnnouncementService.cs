using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public AnnouncementService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, IAnnouncementRepository announcementRepository)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _announcementRepository = announcementRepository;
    }
    
    public async Task<AnnouncementResponseDto> AddAnnouncementAsync(CreateAnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        if (request.IsPremium)
        {
            if (user.PremiumUploadLimit <= 0)
            {
                return null;
            }

            user.PremiumUploadLimit--;
        }
        else if (!request.IsPremium && user.RegularUploadLimit > 0)
        {
            user.RegularUploadLimit--;
        }
        else
        {
            return null;
        }
        
        var response = await _announcementRepository.CreateAnnouncementAsync(user.Id, request);
        
        return response;
    }
    
    public async Task<AnnouncementResponseDto> DeleteDeactivateAnnouncementAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.DeleteInactiveAnnouncementFromDbAsync(user.Id, announcementId);

        return response;
    }

    public async Task<LimitRequestDto> GetUserLimitsAsync()
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetUserLimitsFromDbAsync(user.Id);

        return response;
    }
    
    public async Task<AnnouncementResponseDto> GetAnnouncementByIdAsync(Guid id)
    {
        var response = await _announcementRepository.GetAnnouncementByIdFromDbAsync(id);
        return response;
    }

    public async Task<AnnouncementResponseDto> GetActiveAnnouncementByIdAsync(Guid id)
    {
        var response = await _announcementRepository.GetActiveAnnouncementByIdFromDbAsync(id);
        return response;
    }

    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState)
    {
        var response = await _announcementRepository.GetAnnouncementsFromDbAsync(parameters, announcementState);
        return response;
    }

    public async Task<AnnouncementResponseDto> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.UpdateAnnouncementInDbAsync(user.Id, announcementId, request);
        return response;
    }

    public async Task<AnnouncementResponseDto> MakeAnnouncementActiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementActiveInDbAsync(user.Id, announcementId);

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
    
    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        var response = await _announcementRepository.GetFilteredAnnouncementsFromDbAsync(filterParameters, pagingParameters);
        return response;
    }

    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsByUserIdAsync(PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetAnnouncementsByUserIdFromDbAsync(user.Id, pagingParameters, announcementState);

        return response;
    }
    
    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserIdAsync(PagingParameters pagingParameters)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetAllAnnouncementsByUserIdFromDbAsync(user.Id, pagingParameters);

        return response;
    }
}
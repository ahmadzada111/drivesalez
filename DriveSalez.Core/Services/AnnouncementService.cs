using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFileService _fileService;

    public AnnouncementService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, 
        IAnnouncementRepository announcementRepository, IFileService fileService)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _announcementRepository = announcementRepository;
        _fileService = fileService;
    }
    
    public async Task<AnnouncementResponseDto?> CreateAnnouncementAsync(CreateAnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

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
        
        var response = await _announcementRepository.CreateAnnouncementInDbAsync(user, request);
        
        return response;
    }
    
    public async Task<AnnouncementResponseDto?> DeleteInactivateAnnouncementAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.DeleteInactiveAnnouncementFromDbAsync(user, announcementId);

        await _fileService.DeleteAllFilesAsync(user.Id);
        return response;
    }

    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllPremiumAnnouncementsAsync(PagingParameters pagingParameters)
    {
        var response = await _announcementRepository.GetAllPremiumAnnouncementsFromDbAsync(pagingParameters);
        return response;
    }

    public async Task<LimitRequestDto> GetUserLimitsAsync()
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        return new LimitRequestDto()
        {
            PremiumLimit = user.PremiumUploadLimit,
            RegularLimit = user.RegularUploadLimit,
            AccountBalance = user.AccountBalance
        };
    }
    
    public async Task<AnnouncementResponseDto?> GetAnnouncementByIdAsync(Guid id)
    {
        var response = await _announcementRepository.GetAnnouncementByIdFromDbAsync(id);
        return response;
    }

    public async Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdAsync(Guid id)
    {
        var response = await _announcementRepository.GetActiveAnnouncementByIdFromDbAsync(id);
        return response;
    }

    public async Task<Tuple<IEnumerable<AnnouncementResponseMiniDto>, IEnumerable<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncements(PagingParameters parameters)
    {
        var response = await _announcementRepository.GetAllActiveAnnouncementsFromDbAsync(parameters);
        return response;
    }

    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsForAdminPanelAsync(
        PagingParameters parameters, AnnouncementState announcementState)
    {
        var response =
            await _announcementRepository.GetAllAnnouncementsForAdminPanelFromDbAsync(parameters, announcementState);
        return response;
    }
    
    public async Task<AnnouncementResponseDto?> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.UpdateAnnouncementInDbAsync(user, announcementId, request);
        return response;
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementActiveInDbAsync(user, announcementId);

        return response;
    }
    
    public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementInactiveInDbAsync(user, announcementId);

        return response;
    }
    
    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        var response = await _announcementRepository.GetFilteredAnnouncementsFromDbAsync(filterParameters, pagingParameters);
        return response;
    }

    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserAsync(PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetAnnouncementsByStatesAndByUserFromDbAsync(user, pagingParameters, announcementState);

        return response;
    }
    
    public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserAsync(PagingParameters pagingParameters)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetAllAnnouncementsByUserFromDbAsync(user, pagingParameters);

        return response;
    }
}
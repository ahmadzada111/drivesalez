using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
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

    public async Task<Announcement> AddAnnouncementAsync(AnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            return null;
        }
        
        var response = await _announcementRepository.CreateAnnouncementAsync(user.Id, request);
        
        return response;
    }

    public async Task<Announcement> DeleteDeactivateAnnouncementAsync(int announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            return null;
        }
        
        var response = await _announcementRepository.DeleteInactiveAnnouncementFromDbAsync(user.Id, announcementId);

        return response;
    }

    public Announcement GetAnnouncementById(int id)
    {
        var response =  _announcementRepository.GetAnnouncementByIdFromDb(id);
        return response;
    }

    public IEnumerable<Announcement> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState)
    {
        var response = _announcementRepository.GetAnnouncementsFromDb(parameters, announcementState);
        return response;
    }

    public async Task<Announcement> UpdateAnnouncementAsync(int announcementId, AnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if(user == null)
        {
            return null;
        }

        var response = await _announcementRepository.UpdateAnnouncementInDbAsync(user.Id, announcementId, request);
        return response;
    }

    public async Task<Announcement> ChangeAnnouncementStateAsync(int announcementId, AnnouncementState announcementState)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            return null;
        }
        
        var response = await _announcementRepository.ChangeAnnouncementStateInDbAsync(user.Id, announcementId, announcementState);

        return response;
    }
}
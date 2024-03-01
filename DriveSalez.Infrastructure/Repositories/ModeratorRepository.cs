using AutoMapper;
using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Enums;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Infrastructure.Repositories;

public class ModeratorRepository : IModeratorRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public ModeratorRepository(IMapper mapper, ApplicationDbContext dbContext, ILogger<ModeratorRepository> logger)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<AnnouncementResponseDto?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId)
    {
        try
        {
            _logger.LogInformation($"Making announcement with ID {announcementId} active in DB by moderator");

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId && 
                                              x.AnnouncementState != AnnouncementState.Active);

            if (announcement == null)
            {
                return null;
            }
            
            announcement.AnnouncementState = AnnouncementState.Active;

            var result = _dbContext.Announcements.Update(announcement);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<AnnouncementResponseDto>(announcement);
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error making announcement with ID {announcementId} active in DB by moderator");
            throw;
        }
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId)
    {
        try
        {
            _logger.LogInformation($"Making announcement with ID {announcementId} inactive in DB by moderator");

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId && 
                                              x.AnnouncementState != AnnouncementState.Inactive);

            if (announcement == null)
            {
                return null;
            }
            
            announcement.AnnouncementState = AnnouncementState.Inactive;

            var result = _dbContext.Announcements.Update(announcement);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<AnnouncementResponseDto>(announcement);
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error making announcement with ID {announcementId} inactive in DB by moderator");
            throw;
        }
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementWaitingInDbAsync(ApplicationUser user, Guid announcementId)
    {
        try
        {
            _logger.LogInformation($"Making announcement with ID {announcementId} waiting in DB by moderator");

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId && 
                                              x.AnnouncementState != AnnouncementState.Waiting);

            if (announcement == null)
            {
                return null;
            }
            
            announcement.AnnouncementState = AnnouncementState.Waiting;

            var result = _dbContext.Announcements.Update(announcement);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<AnnouncementResponseDto>(announcement);
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error making announcement with ID {announcementId} inactive in DB by moderator");
            throw;
        }
    }
}
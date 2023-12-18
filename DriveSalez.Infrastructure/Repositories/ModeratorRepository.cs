using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories;

public class ModeratorRepository : IModeratorRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;
    
    public ModeratorRepository(IMapper mapper, ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<AnnouncementResponseDto> MakeAnnouncementActiveInDbAsync(Guid userId, Guid announcementId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

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

        return null;
    }

    public async Task<AnnouncementResponseDto> MakeAnnouncementInactiveInDbAsync(Guid userId, Guid announcementId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

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

        return null;
    }

    public async Task<AnnouncementResponseDto> MakeAnnouncementWaitingInDbAsync(Guid userId, Guid announcementId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

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

        return null;
    }
}
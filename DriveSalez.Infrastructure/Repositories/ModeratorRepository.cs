using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Infrastructure.Repositories
{
    public class ModeratorRepository : IModeratorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly IMapper _mapper;
        
        public ModeratorRepository(ApplicationDbContext dbContext, RoleManager<ApplicationUser> roleManager
        ,IMapper mapper)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        
        public async Task<AnnouncementResponseDto> ChangeAnnouncementStateInDbAsync(Guid userId, Guid announcementId, AnnouncementState announcementState)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }
            
            if (await _roleManager.GetRoleNameAsync(user) == UserType.Moderator.ToString())
            {
                var announcement = _dbContext.Announcements.FirstOrDefault(x => x.Id == announcementId);

                if (announcement == null)
                {
                    return null;
                }

                announcement.AnnouncementState = announcementState;
                announcement.ExpirationDate = DateTimeOffset.Now.AddMonths(1);
                
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<AnnouncementResponseDto>(announcement); 
            }

            return null;
        }
    }
}

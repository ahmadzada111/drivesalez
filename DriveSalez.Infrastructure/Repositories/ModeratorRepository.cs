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

        public ModeratorRepository(ApplicationDbContext dbContext, RoleManager<ApplicationUser> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        
        public async Task<Announcement> ChangeAnnouncementStateInDb(Guid userId, int announcementId, AnnouncementState announcementState)
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

                announcement.AnnoucementState = announcementState;

                await _dbContext.SaveChangesAsync();

                return announcement; 
            }

            return null;
        }
    }
}

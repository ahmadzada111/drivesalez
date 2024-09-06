using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Persistence.Repositories;

internal sealed class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
        
    public AdminRepository(ApplicationDbContext dbContext, ILogger<AdminRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ApplicationUser?> DeleteModeratorFromDbAsync(Guid moderatorId)
    {
        var moderator = await _dbContext.Users
            .Where(x => x.Id == moderatorId)
            .FirstOrDefaultAsync();

        if (moderator == null)
        {
            return null;
        }
            
        var response = _dbContext.Remove(moderatorId);
            
        if (response.State == EntityState.Deleted)
        {
            await _dbContext.SaveChangesAsync();
            return moderator;
        }
            
        throw new InvalidOperationException("Object wasn't deleted");
    }

    public async Task<PaginatedList<ApplicationUser>> GetAllUsersFromDbAsync(PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogError($"Getting all users from db");

            var query = _dbContext.Users
                .Where(x => x.EmailConfirmed)
                .Join(_dbContext.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new
                    {
                        User = user, UserRole = userRole
                    })
                .Where(joined => !_dbContext.Roles
                    .Any(r => r.Name == UserType.Admin.ToString() || r.Name == UserType.Moderator.ToString()))
                .Select(joined => joined.User);
            
            var totalCount = await query.CountAsync();
            var users = await query
                .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();
            
            if (users.IsNullOrEmpty())
            {
                return new PaginatedList<ApplicationUser>();
            }

            var paginatedUsers = PaginatedList<ApplicationUser>.ToPaginatedList(users, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);

            return paginatedUsers;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting all users from db");
            throw;
        }
    }
}
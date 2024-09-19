using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;

namespace DriveSalez.Application.Services;

internal class LimitService : ILimitService
{
    private readonly IUnitOfWork _unitOfWork;

    public LimitService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddLimitToUser(User user, UserType userType)
    {
        var roleLimits = await _unitOfWork.UserRoleLimits.FindAll(x => x.Role.Name == userType.ToString());
        var userRoleLimits = roleLimits.ToList();
        
        if (!userRoleLimits.Any()) return false;
        
        foreach (var roleLimit in userRoleLimits)
        {
            var userLimit = new UserLimit
            {
                UserId = user.Id,
                LimitType = roleLimit.LimitType,
                LimitValue = roleLimit.Value,   
                UsedValue = 0                  
            };

            _unitOfWork.UserLimits.Add(userLimit);
        }
        
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> UpdateUserUploadLimits(LimitType limitType, User user, int value)
    {
        var limit = await _unitOfWork.UserLimits.Find(x => x.User == user && x.LimitType == limitType)
            ?? throw new InvalidOperationException("Limit not found");

        if (limit.UsedValue + value > limit.LimitValue) return false;
        
        limit.UsedValue += value;
        
        _unitOfWork.UserLimits.Update(limit);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface ILimitService
{
    Task<bool> AddLimitToUser(User user, UserType userType);

    Task<bool> UpdateUserUploadLimits(LimitType limitType, User user, int value);
}
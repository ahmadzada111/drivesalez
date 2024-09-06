using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Utilities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IAdminRepository
{
    Task<ApplicationUser?> DeleteModeratorFromDbAsync(Guid moderatorId);

    Task<PaginatedList<ApplicationUser>> GetAllUsersFromDbAsync(PagingParameters pagingParameters);
}
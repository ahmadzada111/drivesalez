using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;
using DriveSalez.SharedKernel.Utilities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IAnnouncementRepository : IGenericRepository<Announcement>
{
    Task<Tuple<IEnumerable<Announcement>, PaginatedList<Announcement>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters pagingParameters);

    Task<PaginatedList<Announcement>> GetFilteredAnnouncementsFromDbAsync(FilterAnnouncementParameters filterParameters, PagingParameters pagingParameters);
}
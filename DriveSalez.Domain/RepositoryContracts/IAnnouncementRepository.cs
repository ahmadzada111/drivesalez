using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IAnnouncementRepository
{
    Task<Announcement> CreateAnnouncementInDbAsync(ApplicationUser user, Announcement request);

    Task<Announcement> UpdateAnnouncementInDbAsync(ApplicationUser user, Guid announcementId, Announcement request);

    Task<Announcement?> DeleteInactiveAnnouncementFromDbAsync(ApplicationUser user, Guid announcementId);

    Task<Announcement?> ChangeAnnouncementStateInDbAsync(ApplicationUser user, Guid announcementId, AnnouncementState announcementState);
        
    Task<Announcement?> GetAnnouncementByIdFromDbAsync(Guid id);

    Task<Announcement?> GetActiveAnnouncementByIdFromDbAsync(Guid id);

    Task<Tuple<IEnumerable<Announcement>, PaginatedList<Announcement>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters parameter);

    Task<PaginatedList<Announcement>> GetFilteredAnnouncementsFromDbAsync(
        FilterParameters filterParameters, PagingParameters pagingParameters);

    Task<PaginatedList<Announcement>> GetAnnouncementsByStatesAndByUserFromDbAsync(ApplicationUser user, PagingParameters pagingParameters, AnnouncementState announcementState);

    Task<PaginatedList<Announcement>> GetAllAnnouncementsByUserFromDbAsync(ApplicationUser user,
        PagingParameters pagingParameters);

    Task<PaginatedList<Announcement>> GetAllPremiumAnnouncementsFromDbAsync(PagingParameters pagingParameters);
        
    Task<PaginatedList<Announcement>> GetAllAnnouncementsForAdminPanelFromDbAsync(PagingParameters parameter, AnnouncementState announcementState);

    Task<ManufactureYear> GetManufactureYearById(int id);

    Task<Make> GetMakeById(int id);

    Task<Model> GetModelById(int id);

    Task<FuelType> GetFuelTypeById(int id);

    Task<GearboxType> GetGearboxById(int id);

    Task<DrivetrainType> GetDrivetrainTypeById(int id);

    Task<BodyType> GetBodyTypeById(int id);

    Task<List<Condition>> GetConditionsByIds(List<int> ids);

    Task<List<Option>> GetOptionsByIds(List<int> ids);

    Task<Color> GetColorById(int id);

    Task<MarketVersion> GetMarketVersionById(int id);

    Task<Country> GetCountryById(int id);

    Task<City> GetCityById(int id);

    Task<bool> CheckAllRelationsInAnnouncement(Announcement request);
}
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

    Task<Announcement?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId);

    Task<Announcement?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId);
        
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

    public Task<ManufactureYear> GetManufactureYearById(int id);

    public Task<Make> GetMakeById(int id);

    public Task<Model> GetModelById(int id);

    public Task<FuelType> GetFuelTypeById(int id);

    public Task<GearboxType> GetGearboxById(int id);

    public Task<DrivetrainType> GetDrivetrainTypeById(int id);

    public Task<BodyType> GetBodyTypeById(int id);

    public Task<List<Condition>> GetConditionsByIds(List<int> ids);

    public Task<List<Option>> GetOptionsByIds(List<int> ids);

    public Task<Color> GetColorById(int id);

    public Task<MarketVersion> GetMarketVersionById(int id);

    public Task<Country> GetCountryById(int id);

    public Task<City> GetCityById(int id);

    public Task<bool> CheckAllRelationsInAnnouncement(Announcement request);
}
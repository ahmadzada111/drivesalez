using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.Abstractions;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Persistence.Specifications;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Persistence.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public AnnouncementRepository(ApplicationDbContext dbContext, IMapper mapper,
        ILogger<AnnouncementRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ManufactureYear> GetManufactureYearById(int id)
    {
        return await _dbContext.ManufactureYears.FindAsync(id) ?? 
        throw new KeyNotFoundException($"Year with id {id} not found");
    }
        
    public async Task<Make> GetMakeById(int id)
    {
        return await _dbContext.Makes.FindAsync(id) ??
        throw new KeyNotFoundException($"Make with id {id} not found");
    }

    public async Task<Model> GetModelById(int id)
    {
        return await _dbContext.Models.FindAsync(id) ??
        throw new KeyNotFoundException($"Model with id {id} not found");
    }

    public async Task<FuelType> GetFuelTypeById(int id)
    {
        return await _dbContext.VehicleFuelTypes.FindAsync(id) ??
        throw new KeyNotFoundException($"Fuel type with id {id} not found");
    }

    public async Task<GearboxType> GetGearboxById(int id)
    {
        return await _dbContext.VehicleGearboxTypes.FindAsync(id) ??
        throw new KeyNotFoundException($"Gearbox with id {id} not found");
    }

    public async Task<DrivetrainType> GetDrivetrainTypeById(int id)
    {
        return await _dbContext.VehicleDriveTrainTypes.FindAsync(id) ??
        throw new KeyNotFoundException($"Drivetrain with id {id} not found");
    }

    public async Task<BodyType> GetBodyTypeById(int id)
    {
        return await _dbContext.VehicleBodyTypes.FindAsync(id) ??
        throw new KeyNotFoundException($"Body type with id {id} not found");
    }

    public async Task<List<Condition>> GetConditionsByIds(List<int> ids)
    {
        return await _dbContext.VehicleDetailsConditions.Where(c => ids.Contains(c.Id)).ToListAsync();
    }

    public async Task<List<Option>> GetOptionsByIds(List<int> ids)
    {
        return await _dbContext.VehicleDetailsOptions.Where(o => ids.Contains(o.Id)).ToListAsync();
    }

    public async Task<Color> GetColorById(int id)
    {
        return await _dbContext.VehicleColors.FindAsync(id) ??
        throw new KeyNotFoundException($"Color with id {id} not found");
    }

    public async Task<MarketVersion> GetMarketVersionById(int id)
    {
        return await _dbContext.VehicleMarketVersions.FindAsync(id) ??
        throw new KeyNotFoundException($"Market version with id {id} not found");
    }

    public async Task<Country> GetCountryById(int id)
    {
        return await _dbContext.Countries.FindAsync(id) ?? 
        throw new KeyNotFoundException($"Country with id {id} not found");
    }

    public async Task<City> GetCityById(int id)
    {
        return await _dbContext.Cities.FindAsync(id) ??
        throw new KeyNotFoundException($"City with id {id} not found");
    }

    public async Task<Announcement> CreateAnnouncementInDbAsync(ApplicationUser user, Announcement announcement)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation($"Creating announcement for user with ID: {user.Id}");
                
            await _dbContext.Announcements.AddAsync(announcement);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
                
            return announcement;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, $"Error creating announcement for user with ID: {user.Id}");
            throw;
        }
    }
        
    public async Task<bool> CheckAllRelationsInAnnouncement(Announcement request)
    {
        try
        {
            _logger.LogInformation("Checking relations in announcement");

            var model = await _dbContext.Models
                .Include(m => m.Make)
                .FirstOrDefaultAsync(m => m.Id == request.Vehicle.Model.Id);

            var city = await _dbContext.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == request.City.Id);

            if (model?.Make.Id != request.Vehicle.Make.Id || city?.Country.Id != request.Country.Id)
            {
                _logger.LogWarning("Relation check failed");
                return false;
            }

            _logger.LogInformation("All relations in announcement are valid.");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking relations in announcement.");
            throw;
        }
    }

    public async Task<Announcement?> GetAnnouncementByIdFromDbAsync(Guid id)
    {
        try
        {
            _logger.LogInformation($"Getting announcement from DB with ID: {id}");

            var response = await _dbContext.Announcements
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (response == null)
            {
                _logger.LogWarning($"Announcement with ID {id} not found");
                return null;
            }

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting announcement from DB with ID: {id}");
            throw;
        }
    }

    public async Task<Announcement?> GetActiveAnnouncementByIdFromDbAsync(Guid id)
    {
        try
        {
            _logger.LogInformation($"Getting active announcement from DB with ID: {id}");

            var response = await _dbContext.Announcements
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == id && x.AnnouncementState == AnnouncementState.Active);

            if (response == null)
            {
                return null;
            }

            response.ViewCount++;

            var result = _dbContext.Update(response);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting active announcement from DB with ID: {id}");
            throw;
        }
    }

    public async Task<PaginatedList<Announcement>> GetAllPremiumAnnouncementsFromDbAsync(PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogInformation($"Getting all premium announcements from DB");

            var query = _dbContext.Announcements
                .AsNoTracking()
                .Where(on => on.AnnouncementState == AnnouncementState.Active && on.IsPremium)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City);

            var totalCount = await query.CountAsync();
            var announcements = await query
                .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();
            
            if (announcements.IsNullOrEmpty())
            {
                return new PaginatedList<Announcement>();
            }

            var paginatedAnnouncements = PaginatedList<Announcement>.Create(announcements, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);

            return paginatedAnnouncements;
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error getting all premium announcements for admin panel from DB");
            throw; 
        }
    }

    public async Task<PaginatedList<Announcement>> GetAllAnnouncementsForAdminPanelFromDbAsync(PagingParameters parameter, AnnouncementState announcementState)
    {
        try
        {
            _logger.LogInformation($"Getting waiting announcements from DB");

            var query = _dbContext.Announcements
                .AsNoTracking()
                .Where(on => on.AnnouncementState == announcementState)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City);
            
            var totalCount = await query.CountAsync();
            var announcements = await query
                .Skip((parameter.PageIndex - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();
            
            if (announcements.IsNullOrEmpty())
            {
                return new PaginatedList<Announcement>();
            }

            var paginatedAnnouncements = PaginatedList<Announcement>.Create(announcements, parameter.PageIndex, parameter.PageSize, totalCount);

            return paginatedAnnouncements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all announcements for admin panel from DB");
            throw;
        }
    }
        
    private IQueryable<Announcement> BuildAnnouncementsQuery(bool isPremium = false)
    {
        var query = _dbContext.Announcements
            .AsNoTracking()
            .Where(on => on.AnnouncementState == AnnouncementState.Active);

        if (isPremium)
        {
            query = query.Where(on => on.IsPremium);
        }

        query = query
            .Include(x => x.Owner)
            .Include(x => x.Owner.PhoneNumbers)
            .Include(x => x.Vehicle)
            .Include(x => x.ImageUrls)
            .Include(x => x.Vehicle.VehicleDetail.Year)
            .Include(x => x.Vehicle.Make)
            .Include(x => x.Vehicle.Model)
            .Include(x => x.Vehicle.VehicleDetail.FuelType)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.BodyType)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.DrivetrainType)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.GearboxType)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.Color)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.MarketVersion)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.Options)
            .Include(x => x.Vehicle.VehicleDetail)
            .ThenInclude(x => x.Conditions)
            .Include(x => x.Country)
            .Include(x => x.City);

        return query;
    }

    public async Task<Tuple<IEnumerable<Announcement>, PaginatedList<Announcement>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters parameter)
    {
        try
        {
            _logger.LogInformation("Getting announcements from DB");
            
            var allPremiumAnnouncements = await BuildAnnouncementsQuery(true).ToListAsync();
            
            var random = new Random();
            var premiumAnnouncements = allPremiumAnnouncements.OrderBy(x => random.Next()).Take(8).ToList();
            
            var nonPremiumQuery = BuildAnnouncementsQuery(false);
            var totalNonPremiumCount = await nonPremiumQuery.CountAsync();
            var nonPremiumAnnouncements = await nonPremiumQuery
                .Skip((parameter.PageIndex - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            var paginatedNonPremiumAnnouncements = PaginatedList<Announcement>.Create(nonPremiumAnnouncements, parameter.PageIndex, parameter.PageSize, totalNonPremiumCount);

            return Tuple.Create<IEnumerable<Announcement>, PaginatedList<Announcement>>(premiumAnnouncements, paginatedNonPremiumAnnouncements);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all announcements from DB");
            throw;
        }
    }
    
    public async Task<Announcement> UpdateAnnouncementInDbAsync(ApplicationUser user, Guid announcementId, Announcement request)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation($"Updating announcement in DB with ID {announcementId} for user with ID {user.Id}");

            var announcement = await _dbContext.Announcements
                .Where(x => x.Id == announcementId)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .FirstOrDefaultAsync();

            if (announcement == null)
            {
                throw new KeyNotFoundException($"Announcement with ID {announcementId} not found.");
            }

            if (!await CheckAllRelationsInAnnouncement(request))
            {
                throw new ValidationException("Invalid relationships in the announcement.");
            }

            var currentViewCount = announcement.ViewCount;
            var currentId = announcement.Id;

            _dbContext.Entry(announcement).CurrentValues.SetValues(request);
            announcement.Id = currentId;
            announcement.ViewCount = currentViewCount;

            _dbContext.Announcements.Update(announcement);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return announcement;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, $"Error updating announcement with ID {announcementId} for user with ID {user.Id}");
            throw;
        }
    }

    public async Task<Announcement?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId)
    {
        try
        {
            _logger.LogInformation($"Making announcement with ID {announcementId} active in DB for user with ID {user.Id}");

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId &&
                                              x.Owner == user &&
                                              x.AnnouncementState != AnnouncementState.Active);

            if (announcement == null)
            {
                return null;
            }

            announcement.AnnouncementState = AnnouncementState.Active;
            announcement.ExpirationDate = DateTimeOffset.Now.AddMonths(1);

            var result = _dbContext.Announcements.Update(announcement);

            return announcement;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error making announcement active with ID {announcementId} for user with ID {user.Id}");
            throw;
        }
    }

    public async Task<Announcement?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId)
    {
        try
        {
            _logger.LogInformation($"Making announcement with ID {announcementId} inactive in DB for user with ID {user.Id}");

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId &&
                                              x.Owner == user &&
                                              x.AnnouncementState != AnnouncementState.Inactive);

            if (announcement == null)
            {
                return null;
            }

            announcement.AnnouncementState = AnnouncementState.Inactive;

            var result = _dbContext.Announcements.Update(announcement);

            return announcement;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error making announcement inactive with ID {announcementId} for user with ID {user.Id}");
            throw;
        }
    }

    public async Task<Announcement?> DeleteInactiveAnnouncementFromDbAsync(ApplicationUser user,
        Guid announcementId)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            
        try
        {
            _logger.LogInformation($"Deleting announcement with ID {announcementId} from DB for user with ID {user.Id}");

            var announcement = await _dbContext.Announcements
                .Where(x => x.Id == announcementId &&
                            x.AnnouncementState == AnnouncementState.Inactive &&
                            x.Owner.Id == user.Id)
                .Include(a => a.Owner)
                .Include(a => a.ImageUrls)
                .FirstOrDefaultAsync();
                
            if (announcement == null)
            {
                return null;
            }

            _dbContext.ImageUrls.RemoveRange(announcement.ImageUrls);
            var response = _dbContext.Announcements.Remove(announcement);

            return announcement;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error deleting announcement with ID {announcementId} for user with ID {user.Id}");
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<PaginatedList<Announcement>> GetAnnouncementsByStatesAndByUserFromDbAsync(ApplicationUser user,
        PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        try
        {
            _logger.LogInformation($"Getting announcements by user ID {user.Id} from DB");

            var query = _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.Owner.Id == user.Id && x.AnnouncementState == announcementState)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City);
                

            var totalCount = await query.CountAsync();
            var announcements = await query
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();
            
            if (announcements.IsNullOrEmpty())
            {
                return new PaginatedList<Announcement>();
            }

            var paginatedAnnouncements = PaginatedList<Announcement>.Create(announcements, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);

            return paginatedAnnouncements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting announcements for user with ID {user.Id}");
            throw;
        }
    }

    public async Task<PaginatedList<Announcement>> GetAllAnnouncementsByUserFromDbAsync(ApplicationUser user,
        PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogInformation($"Getting all announcements by user ID {user.Id} from DB");

            var query = _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.Owner.Id == user.Id)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City);
            
            var totalCount = await query.CountAsync();
            
            var announcements = await query
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();
            
            if (announcements.IsNullOrEmpty())
            {
                return new PaginatedList<Announcement>();
            }

            var paginatedAnnouncements = PaginatedList<Announcement>.Create(announcements, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);

            return paginatedAnnouncements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting all announcements for user with ID {user.Id}");
            throw;
        }
    }

    private List<ISpecification<Announcement>> BuildSpecifications(FilterParameters filterParameters)
    {
        var specs = new List<ISpecification<Announcement>>();

        if (filterParameters.IsBrandNew.HasValue)
        {
            specs.Add(new AnnouncementByIsBrandNewSpecification(filterParameters.IsBrandNew));
        }

        if (filterParameters.FromYearId.HasValue || filterParameters.ToYearId.HasValue)
        {
            specs.Add(new AnnouncementByYearRangeSpecification(filterParameters.FromYearId, filterParameters.ToYearId));
        }

        if (filterParameters.MakeId.HasValue)
        {
            specs.Add(new AnnouncementByMakeSpecification(filterParameters.MakeId));
        }

        if (filterParameters.ModelsIds != null && filterParameters.ModelsIds.Any())
        {
            specs.Add(new AnnouncementByModelsSpecification(filterParameters.ModelsIds));
        }

        if (filterParameters.FuelTypesIds != null && filterParameters.FuelTypesIds.Any())
        {
            specs.Add(new AnnouncementByFuelTypesSpecification(filterParameters.FuelTypesIds));
        }

        if (filterParameters.BodyTypesIds != null && filterParameters.BodyTypesIds.Any())
        {
            specs.Add(new AnnouncementByBodyTypesSpecification(filterParameters.BodyTypesIds));
        }

        if (filterParameters.ColorsIds != null && filterParameters.ColorsIds.Any())
        {
            specs.Add(new AnnouncementByColorsSpecification(filterParameters.ColorsIds));
        }

        if (filterParameters.FromHorsePower.HasValue || filterParameters.ToHorsePower.HasValue)
        {
            specs.Add(new AnnouncementByHorsePowerRangeSpecification(filterParameters.FromHorsePower, filterParameters.ToHorsePower));
        }

        if (filterParameters.GearboxTypesIds != null && filterParameters.GearboxTypesIds.Any())
        {
            specs.Add(new AnnouncementByGearboxesSpecification(filterParameters.GearboxTypesIds));
        }

        if (filterParameters.DriveTrainTypesIds != null && filterParameters.DriveTrainTypesIds.Any())
        {
            specs.Add(new AnnouncementByDrivetrainsSpecification(filterParameters.DriveTrainTypesIds));
        }

        if (filterParameters.ConditionsIds != null && filterParameters.ConditionsIds.Any())
        {
            specs.Add(new AnnouncementByConditionsSpecification(filterParameters.ConditionsIds));
        }

        if (filterParameters.MarketVersionsIds != null && filterParameters.MarketVersionsIds.Any())
        {
            specs.Add(new AnnouncementByMarketVersionsSpecification(filterParameters.MarketVersionsIds));
        }

        if (filterParameters.SeatCount.HasValue)
        {
            specs.Add(new AnnouncementBySeatCountSpecification(filterParameters.SeatCount));
        }

        if (filterParameters.OptionsIds != null && filterParameters.OptionsIds.Any())
        {
            specs.Add(new AnnouncementByOptionsSpecification(filterParameters.OptionsIds));
        }

        if (filterParameters.FromEngineVolume.HasValue || filterParameters.ToEngineVolume.HasValue)
        {
            specs.Add(new AnnouncementByEngineVolumeRangeSpecification(filterParameters.FromEngineVolume, filterParameters.ToEngineVolume));
        }

        if (filterParameters.FromMileage.HasValue || filterParameters.ToMileage.HasValue)
        {
            specs.Add(new AnnouncementByMileageRangeSpecification(filterParameters.FromMileage, filterParameters.ToMileage));
        }

        if (!string.IsNullOrEmpty(filterParameters.MileageType))
        {
            specs.Add(new AnnouncementByMileageTypeSpecification(filterParameters.MileageType));
        }

        if (filterParameters.Barter.HasValue)
        {
            specs.Add(new AnnouncementByBarterSpecification(filterParameters.Barter));
        }

        if (filterParameters.OnCredit.HasValue)
        {
            specs.Add(new AnnouncementByOnCreditSpecification(filterParameters.OnCredit));
        }

        if (filterParameters.FromPrice.HasValue || filterParameters.ToPrice.HasValue)
        {
            specs.Add(new AnnouncementByPriceRangeSpecification(filterParameters.FromPrice, filterParameters.ToPrice));
        }

        if (filterParameters.CountryId.HasValue)
        {
            specs.Add(new AnnouncementByCountrySpecification(filterParameters.CountryId));
        }

        if (filterParameters.CitiesIds != null && filterParameters.CitiesIds.Any())
        {
            specs.Add(new AnnouncementByCitiesSpecification(filterParameters.CitiesIds));
        }

        return specs;
    }

    public async Task<PaginatedList<Announcement>> GetFilteredAnnouncementsFromDbAsync(
    FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogInformation("Getting filtered announcements from DB");
            
            var query = _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.AnnouncementState == AnnouncementState.Active)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.VehicleDetail.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.VehicleDetail.FuelType)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetail)
                    .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City);
            
            var filterableQuery = query.AsQueryable();
            
            var specs = BuildSpecifications(filterParameters);
            var filter = new Filter<Announcement>();
            
            foreach (var spec in specs)
            {
                filterableQuery = filter.ApplyFilter(filterableQuery, spec);
            }
            
            var totalCount = await filterableQuery.CountAsync();
            var announcements = await filterableQuery
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();
            
            return new PaginatedList<Announcement>(announcements, totalCount, pagingParameters.PageIndex, pagingParameters.PageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving announcements.");
            throw; 
        }
    }
}
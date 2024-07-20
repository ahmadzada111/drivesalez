using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.Pagination;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Persistence.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ILogger _logger;

    public AnnouncementRepository(ApplicationDbContext dbContext, IMapper mapper,
        IFileService fileService, ILogger<AnnouncementRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<ManufactureYear> GetManufactureYearById(int id)
    {
        return await _dbContext.ManufactureYears.FindAsync(id);
    }
        
    public async Task<Make> GetMakeById(int id)
    {
        return await _dbContext.Makes.FindAsync(id);
    }

    public async Task<Model> GetModelById(int id)
    {
        return await _dbContext.Models.FindAsync(id);
    }

    public async Task<VehicleFuelType> GetFuelTypeById(int id)
    {
        return await _dbContext.VehicleFuelTypes.FindAsync(id);
    }

    public async Task<VehicleGearboxType> GetGearboxById(int id)
    {
        return await _dbContext.VehicleGearboxTypes.FindAsync(id);
    }

    public async Task<VehicleDrivetrainType> GetDrivetrainTypeById(int id)
    {
        return await _dbContext.VehicleDriveTrainTypes.FindAsync(id);
    }

    public async Task<VehicleBodyType> GetBodyTypeById(int id)
    {
        return await _dbContext.VehicleBodyTypes.FindAsync(id);
    }

    public async Task<List<VehicleCondition>> GetConditionsByIds(List<int> ids)
    {
        return await _dbContext.VehicleDetailsConditions.Where(c => ids.Contains(c.Id)).ToListAsync();
    }

    public async Task<List<VehicleOption>> GetOptionsByIds(List<int> ids)
    {
        return await _dbContext.VehicleDetailsOptions.Where(o => ids.Contains(o.Id)).ToListAsync();
    }

    public async Task<VehicleColor> GetColorById(int id)
    {
        return await _dbContext.VehicleColors.FindAsync(id);
    }

    public async Task<VehicleMarketVersion> GetMarketVersionById(int id)
    {
        return await _dbContext.VehicleMarketVersions.FindAsync(id);
    }

    public async Task<Country> GetCountryById(int id)
    {
        return await _dbContext.Countries.FindAsync(id);
    }

    public async Task<City> GetCityById(int id)
    {
        return await _dbContext.Cities.FindAsync(id);
    }

    public async Task<Currency> GetCurrencyById(int id)
    {
        return await _dbContext.Currencies.FindAsync(id);
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
                
            var entry = _dbContext.Entry(announcement);
            if (entry.State == EntityState.Added)
            {
                return announcement;
            }
                
            throw new InvalidOperationException("Failed to add announcement.");
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

            var currency = await _dbContext.Currencies.FindAsync(request.Currency.Id);

            if (model?.Make.Id != request.Vehicle.Make.Id || city?.Country.Id != request.Country.Id || currency == null)
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
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (response == null)
            {
                _logger.LogWarning($"Announcement not found with ID: {id}");
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
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
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

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return response;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting active announcement from DB with ID: {id}");
            throw;
        }
    }

    public async Task<IEnumerable<Announcement>> GetAllPremiumAnnouncementsFromDbAsync(PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogInformation($"Getting all premium announcements from DB");

            var waitingAnnouncements = await _dbContext.Announcements
                .AsNoTracking()
                .Where(on => on.AnnouncementState == AnnouncementState.Active && on.IsPremium)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();

            if (waitingAnnouncements.IsNullOrEmpty())
            {
                return Enumerable.Empty<Announcement>();
            }

            return waitingAnnouncements; 
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Error getting all premium announcements for admin panel from DB");
            throw; 
        }
    }

    public async Task<IEnumerable<Announcement>> GetAllAnnouncementsForAdminPanelFromDbAsync(PagingParameters parameter, AnnouncementState announcementState)
    {
        try
        {
            _logger.LogInformation($"Getting waiting announcements from DB");

            var waitingAnnouncements = await _dbContext.Announcements
                .AsNoTracking()
                .Where(on => on.AnnouncementState == announcementState)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            if (waitingAnnouncements.IsNullOrEmpty())
            {
                return Enumerable.Empty<Announcement>();
            }

            return waitingAnnouncements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all announcements for admin panel from DB");
            throw;
        }
    }
        
    public async Task<Tuple<IEnumerable<Announcement>, IEnumerable<Announcement>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters parameter)
    {
        try
        {
            _logger.LogInformation($"Getting announcements from DB");

            var premiumAnnouncements = await _dbContext.Announcements
                .AsNoTracking()
                .Where(on => on.AnnouncementState == AnnouncementState.Active && on.IsPremium)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .Take(8)
                .ToListAsync();

            var allAnnouncements = await _dbContext.Announcements
                .AsNoTracking()
                .Where(on => on.AnnouncementState == AnnouncementState.Active)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();
                
            if (premiumAnnouncements.IsNullOrEmpty() && allAnnouncements.IsNullOrEmpty())
            {
                return Tuple.Create(Enumerable.Empty<Announcement>(), Enumerable.Empty<Announcement>());
            }
                
            return Tuple.Create<IEnumerable<Announcement>, IEnumerable<Announcement>>(allAnnouncements, premiumAnnouncements);
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
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
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

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return announcement;
            }

            throw new InvalidOperationException("Object wasn't modified");
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

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<Announcement>(announcement);
            }

            throw new InvalidOperationException("Object wasn't modified");
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

            if (response.State == EntityState.Deleted)
            {
                await transaction.CommitAsync();
                await _dbContext.SaveChangesAsync();
                return announcement;
            }

            throw new InvalidOperationException("Object wasn't deleted");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error deleting announcement with ID {announcementId} for user with ID {user.Id}");
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Announcement>> GetAnnouncementsByStatesAndByUserFromDbAsync(ApplicationUser user,
        PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        try
        {
            _logger.LogInformation($"Getting announcements by user ID {user.Id} from DB");

            var announcements = await _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.Owner.Id == user.Id && x.AnnouncementState == announcementState)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();

            if (announcements.IsNullOrEmpty())
            {
                return Enumerable.Empty<Announcement>();
            }

            return announcements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting announcements for user with ID {user.Id}");
            throw;
        }
    }

    public async Task<IEnumerable<Announcement>> GetAllAnnouncementsByUserFromDbAsync(ApplicationUser user,
        PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogInformation($"Getting all announcements by user ID {user.Id} from DB");

            var announcement = await _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.Owner.Id == user.Id)             .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();

            if (announcement.IsNullOrEmpty())
            {
                return Enumerable.Empty<Announcement>();
            }

            return announcement;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting all announcements for user with ID {user.Id}");
            throw;
        }
    }

    public async Task<IEnumerable<Announcement>> GetFilteredAnnouncementsFromDbAsync(
        FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogInformation($"Getting filtered announcements from DB");

            var filteredAnnouncements = _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.AnnouncementState == AnnouncementState.Active)
                .Include(x => x.Owner)
                .Include(x => x.Owner.PhoneNumbers)
                .Include(x => x.Vehicle)
                .Include(x => x.Currency)
                .Include(x => x.ImageUrls)
                .Include(x => x.Vehicle.Year)
                .Include(x => x.Vehicle.Make)
                .Include(x => x.Vehicle.Model)
                .Include(x => x.Vehicle.FuelType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.BodyType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Color)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Options)
                .Include(x => x.Vehicle.VehicleDetails)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize);

            if (filterParameters.FromYearId != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Vehicle.Year.Id >= filterParameters.FromYearId);
            }

            if (filterParameters.ToYearId != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Vehicle.Year.Id <= filterParameters.ToYearId);
            }

            if (filterParameters.MakeId != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Vehicle.Make.Id == filterParameters.MakeId);
            }

            if (filterParameters.IsBrandNew != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Vehicle.IsBrandNew == filterParameters.IsBrandNew);
            }

            if (filterParameters.MakeId != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Vehicle.Make.Id == filterParameters.MakeId);
            }

            if (filterParameters.FromHorsePower != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x =>
                    x.Vehicle.VehicleDetails.HorsePower >= filterParameters.FromHorsePower);
            }

            if (filterParameters.ToHorsePower != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x =>
                    x.Vehicle.VehicleDetails.HorsePower <= filterParameters.ToHorsePower);
            }

            if (filterParameters.SeatCount != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x =>
                        x.Vehicle.VehicleDetails.SeatCount == filterParameters.SeatCount);
            }

            if (filterParameters.FromEngineVolume != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x =>
                    x.Vehicle.VehicleDetails.EngineVolume >= filterParameters.FromEngineVolume);
            }

            if (filterParameters.ToEngineVolume != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x =>
                    x.Vehicle.VehicleDetails.EngineVolume <= filterParameters.ToEngineVolume);
            }

            if (filterParameters.FromMileage != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x =>
                        x.Vehicle.VehicleDetails.MileAge >= filterParameters.FromMileage);
            }

            if (filterParameters.ToMileage != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x =>
                        x.Vehicle.VehicleDetails.MileAge <= filterParameters.ToMileage);
            }

            if (filterParameters.MileageType != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x =>
                    x.Vehicle.VehicleDetails.MileageType == filterParameters.MileageType);
            }

            if (filterParameters.Barter != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x => x.Barter == filterParameters.Barter);
            }

            if (filterParameters.OnCredit != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x => x.OnCredit == filterParameters.OnCredit);
            }

            if (filterParameters.FromPrice != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x => x.Price >= filterParameters.FromPrice);
            }

            if (filterParameters.ToPrice != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x => x.Price <= filterParameters.ToPrice);
            }

            if (filterParameters.OnCredit != null)
            {
                filteredAnnouncements = filteredAnnouncements.Where(x => x.OnCredit == filterParameters.OnCredit);
            }

            if (filterParameters.CurrencyId != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Currency.Id == filterParameters.CurrencyId);
            }

            if (filterParameters.CountryId != null)
            {
                filteredAnnouncements =
                    filteredAnnouncements.Where(x => x.Country.Id == filterParameters.CountryId);
            }

            if (filterParameters.ModelsIds != null && filterParameters.ModelsIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.ModelsIds.Contains(x.Vehicle.Model.Id));
            }

            if (filterParameters.FuelTypesIds != null && filterParameters.FuelTypesIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.FuelTypesIds.Contains(x.Vehicle.FuelType.Id));
            }

            if (filterParameters.BodyTypesIds != null && filterParameters.BodyTypesIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.BodyTypesIds.Contains(x.Vehicle.VehicleDetails.BodyType.Id));
            }

            if (filterParameters.ColorsIds != null && filterParameters.ColorsIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.ColorsIds.Contains(x.Vehicle.VehicleDetails.BodyType.Id));
            }

            if (filterParameters.GearboxTypesIds != null && filterParameters.GearboxTypesIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.GearboxTypesIds.Contains(x.Vehicle.VehicleDetails.GearboxType.Id));
            }

            if (filterParameters.DriveTrainTypesIds != null && filterParameters.DriveTrainTypesIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(
                        x => filterParameters.DriveTrainTypesIds.Contains(
                            x.Vehicle.VehicleDetails.DrivetrainType.Id));
            }

            if (filterParameters.MarketVersionsIds != null && filterParameters.MarketVersionsIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.MarketVersionsIds.Contains(x.Vehicle.VehicleDetails.MarketVersion
                        .Id));
            }

            if (filterParameters.CitiesIds != null && filterParameters.CitiesIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => filterParameters.CitiesIds.Contains(x.City.Id));
            }

            if (filterParameters.OptionsIds != null && filterParameters.OptionsIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => x.Vehicle.VehicleDetails.Options
                        .All(option => filterParameters.OptionsIds.Contains(option.Id)));
            }

            if (filterParameters.ConditionsIds != null && filterParameters.ConditionsIds.Any())
            {
                filteredAnnouncements = filteredAnnouncements
                    .Where(x => x.Vehicle.VehicleDetails.Conditions
                        .All(condition => filterParameters.ConditionsIds.Contains(condition.Id)));
            }

            await filteredAnnouncements.ToListAsync();

            if (filteredAnnouncements.IsNullOrEmpty())
            {
                return Enumerable.Empty<Announcement>();
            }

            return filteredAnnouncements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting filtered announcements");
            throw;
        }
    }
}
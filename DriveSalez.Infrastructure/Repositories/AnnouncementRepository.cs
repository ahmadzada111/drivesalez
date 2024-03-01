using AutoMapper;
using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Enums;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Infrastructure.Repositories
{
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

        // public async Task<LimitRequestDto> GetUserLimitsFromDbAsync(ApplicationUser user)
        // {
        //     try
        //     {
        //         _logger.LogInformation($"Getting user limits from DB for user with ID: {user.Id}");
        //
        //         // var user = await _dbContext.Users
        //         //     .Where(x => x.Id == userId)
        //         //     .FirstOrDefaultAsync();
        //
        //         // if (user == null)
        //         // {
        //         //     _logger.LogWarning($"User not found with ID: {user.Id}");
        //         //     throw new UserNotFoundException("User not found");
        //         // }
        //
        //         return new LimitRequestDto()
        //         {
        //             PremiumLimit = user.PremiumUploadLimit,
        //             RegularLimit = user.RegularUploadLimit,
        //             AccountBalance = user.AccountBalance
        //         };
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError(e, $"Error getting user limits from DB for user with ID: {user.Id}");
        //         throw;
        //     }
        // }

        public async Task<AnnouncementResponseDto> CreateAnnouncementInDbAsync(ApplicationUser user, CreateAnnouncementDto request)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                _logger.LogInformation($"Creating announcement for user with ID: {user.Id}");

                // var user = await _dbContext.Users
                //     .Include(x => x.PhoneNumbers)
                //     .FirstOrDefaultAsync(x => x.Id == userId);

                // if (user == null)
                // {
                //     _logger.LogWarning($"User not found with ID: {user.Id}");
                //     throw new UserNotFoundException("User not found");
                // }

                if (!await CheckAllRelationsInAnnouncement(request))
                {
                    throw new ArgumentException("Invalid relations in the announcement request");
                }
                
                var announcement = new Announcement()
                {
                    Vehicle = new Vehicle()
                    {
                        Year = await _dbContext.ManufactureYears.FindAsync(request.YearId),
                        Make = await _dbContext.Makes.FindAsync(request.MakeId),
                        Model = await _dbContext.Models.FindAsync(request.ModelId),
                        FuelType = await _dbContext.VehicleFuelTypes.FindAsync(request.FuelTypeId),
                        IsBrandNew = request.IsBrandNew,

                        VehicleDetails = new VehicleDetails()
                        {
                            BodyType = await _dbContext.VehicleBodyTypes.FindAsync(request.BodyTypeId),
                            Color = await _dbContext.VehicleColors.FindAsync(request.ColorId),
                            HorsePower = request.HorsePower,
                            GearboxType = await _dbContext.VehicleGearboxTypes.FindAsync(request.GearboxId),
                            DrivetrainType = await _dbContext.VehicleDriveTrainTypes.FindAsync(request.DrivetrainTypeId),
                            MarketVersion = await _dbContext.VehicleMarketVersions.FindAsync(request.MarketVersionId),
                            OwnerQuantity = request.OwnerQuantity,
                            Options = await _dbContext.VehicleDetailsOptions
                                .Where(option => request.OptionsIds.Contains(option.Id))
                                .ToListAsync(),
                            Conditions = await _dbContext.VehicleDetailsConditions
                                .Where(condition => request.ConditionsIds.Contains(condition.Id))
                                .ToListAsync(),
                            SeatCount = request.SeatCount,
                            VinCode = request.VinCode,
                            EngineVolume = request.EngineVolume,
                            MileAge = request.Mileage,
                            MileageType = request.MileageType
                        }
                    },
                    ViewCount = 0,
                    ImageUrls = await _fileService.UploadFilesAsync(request.ImageData),
                    ExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Barter = request.Barter,
                    OnCredit = request.OnCredit,
                    Description = request.Description,
                    Price = request.Price,
                    Currency = await _dbContext.Currencies.FindAsync(request.CurrencyId),
                    Country = await _dbContext.Countries.FindAsync(request.CountryId),
                    City = await _dbContext.Cities.FindAsync(request.CityId),
                    IsPremium = request.IsPremium,
                    PremiumExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Owner = user
                };

                user.Announcements?.Add(announcement);
                var response = await _dbContext.Announcements.AddAsync(announcement);

                if (response.State == EntityState.Added)
                {
                    await transaction.CommitAsync();
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<AnnouncementResponseDto>(announcement);
                }

                throw new InvalidOperationException("Object wasn't added");
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError(e, $"Error getting user limits from DB for user with ID: {user.Id}");
                throw;
            }
        }

        private async Task<bool> CheckAllRelationsInAnnouncement(CreateAnnouncementDto request)
        {
            try
            {
                _logger.LogInformation("Checking relations in announcement...");

                var model = await _dbContext.Models.FindAsync(request.ModelId);
                var make = await _dbContext.Makes.FindAsync(request.MakeId);
                var country = await _dbContext.Countries.FindAsync(request.CountryId);
                var city = await _dbContext.Cities.FindAsync(request.CityId);
                var currency = await _dbContext.Currencies.FindAsync(request.CurrencyId);
                var distanceUnit = request.MileageType;

                if (model.Make != make || country != city.Country || currency == null ||
                    distanceUnit != DistanceUnit.KM && distanceUnit != DistanceUnit.MI)
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

        public async Task<AnnouncementResponseDto?> GetAnnouncementByIdFromDbAsync(Guid id)
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

                return _mapper.Map<AnnouncementResponseDto>(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting announcement from DB with ID: {id}");
                throw;
            }
        }

        public async Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdFromDbAsync(Guid id)
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
                    return _mapper.Map<AnnouncementResponseDto>(response);
                }

                throw new InvalidOperationException("Object wasn't modified");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting active announcement from DB with ID: {id}");
                throw;
            }
        }

        public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsForAdminPanelFromDbAsync(PagingParameters parameter, AnnouncementState announcementState)
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
                    return Enumerable.Empty<AnnouncementResponseMiniDto>();
                }

                return _mapper.Map<IEnumerable<AnnouncementResponseMiniDto>>(waitingAnnouncements);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting all announcements for admin panel from DB");
                throw;
            }
        }
        
        public async Task<Tuple<IEnumerable<AnnouncementResponseMiniDto>, IEnumerable<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters parameter)
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
                    return Tuple.Create(Enumerable.Empty<AnnouncementResponseMiniDto>(), Enumerable.Empty<AnnouncementResponseMiniDto>());
                }
                
                var regularAnnouncementDtos = _mapper.Map<List<AnnouncementResponseMiniDto>>(premiumAnnouncements);
                var premiumAnnouncementDtos = _mapper.Map<List<AnnouncementResponseMiniDto>>(allAnnouncements);
                
                return Tuple.Create<IEnumerable<AnnouncementResponseMiniDto>, IEnumerable<AnnouncementResponseMiniDto>>(regularAnnouncementDtos, premiumAnnouncementDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting all announcements from DB");
                throw;
            }
        }

        public async Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(ApplicationUser user, Guid announcementId,
            UpdateAnnouncementDto request)
        {
            try
            {
                _logger.LogInformation($"Updating announcement in DB with ID {announcementId} for user with ID {user.Id}");

                // var user = await _dbContext.Users.FindAsync(userId);

                // if (user == null)
                // {
                //     throw new UserNotFoundException("User not found");
                // }

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

                announcement.Id = announcementId;
                announcement.Vehicle.Year = await _dbContext.ManufactureYears.FindAsync(request.YearId);
                announcement.Vehicle.Make = await _dbContext.Makes.FindAsync(request.MakeId);
                announcement.Vehicle.Model = await _dbContext.Models.FindAsync(request.ModelId);
                announcement.Vehicle.FuelType = await _dbContext.VehicleFuelTypes.FindAsync(request.FuelTypeId);
                announcement.Vehicle.IsBrandNew = request.IsBrandNew;
                announcement.Vehicle.VehicleDetails.BodyType = await _dbContext.VehicleBodyTypes.FindAsync(request.BodyTypeId);
                announcement.Vehicle.VehicleDetails.Color = await _dbContext.VehicleColors.FindAsync(request.ColorId);
                announcement.Vehicle.VehicleDetails.HorsePower = request.HorsePower;
                announcement.Vehicle.VehicleDetails.GearboxType = await _dbContext.VehicleGearboxTypes.FindAsync(request.GearboxId);
                announcement.Vehicle.VehicleDetails.DrivetrainType = await _dbContext.VehicleDriveTrainTypes.FindAsync(request.DrivetrainTypeId);
                announcement.Vehicle.VehicleDetails.MarketVersion = await _dbContext.VehicleMarketVersions.FindAsync(request.MarketVersionId);
                announcement.Vehicle.VehicleDetails.OwnerQuantity = request.OwnerQuantity;
                announcement.Vehicle.VehicleDetails.SeatCount = request.SeatCount;
                announcement.Vehicle.VehicleDetails.VinCode = request.VinCode;
                announcement.Vehicle.VehicleDetails.EngineVolume = request.EngineVolume;
                announcement.Vehicle.VehicleDetails.MileAge = request.Mileage;
                announcement.Vehicle.VehicleDetails.MileageType = request.MileageType;
                // announcement.ImageUrls = await _fileService.UploadFilesAsync(request.I);
                announcement.Barter = request.Barter;
                announcement.OnCredit = request.OnCredit;
                announcement.Description = request.Description;
                announcement.Price = request.Price;
                announcement.Currency = await _dbContext.Currencies.FindAsync(request.CurrencyId);
                announcement.Country = await _dbContext.Countries.FindAsync(request.CountryId);
                announcement.City = await _dbContext.Cities.FindAsync(request.CityId);
                announcement.Owner = user;

                var result = _dbContext.Update(announcement);

                if (result.State == EntityState.Modified)
                {
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<AnnouncementResponseDto>(announcement);
                }

                throw new InvalidOperationException("Object wasn't modified");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error updating announcement with ID {announcementId} for user with ID {user.Id}");
                throw;
            }
        }

        public async Task<AnnouncementResponseDto?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId)
        {
            try
            {
                _logger.LogInformation($"Making announcement with ID {announcementId} active in DB for user with ID {user.Id}");

                // var user = await _dbContext.Users.FindAsync(userId);

                // if (user == null)
                // {
                //     throw new UserNotFoundException("User not found");
                // }

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
                    return _mapper.Map<AnnouncementResponseDto>(announcement);
                }

                throw new InvalidOperationException("Object wasn't modified");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error making announcement active with ID {announcementId} for user with ID {user.Id}");
                throw;
            }
        }

        public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId)
        {
            try
            {
                _logger.LogInformation($"Making announcement with ID {announcementId} inactive in DB for user with ID {user.Id}");

                // var user = await _dbContext.Users.FindAsync(userId);

                // if (user == null)
                // {
                //     throw new UserNotFoundException("User not found");
                // }

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
                    return _mapper.Map<AnnouncementResponseDto>(announcement);
                }

                throw new InvalidOperationException("Object wasn't modified");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error making announcement inactive with ID {announcementId} for user with ID {user.Id}");
                throw;
            }
        }

        public async Task<AnnouncementResponseDto?> DeleteInactiveAnnouncementFromDbAsync(ApplicationUser user,
            Guid announcementId)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            
            try
            {
                _logger.LogInformation($"Deleting announcement with ID {announcementId} from DB for user with ID {user.Id}");

                // var user = await _dbContext.Users.FindAsync(user);

                // if (user == null)
                // {
                //     throw new UserNotFoundException("User not found");
                // }

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
                    return _mapper.Map<AnnouncementResponseDto>(announcement);
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

        public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserFromDbAsync(ApplicationUser user,
            PagingParameters pagingParameters, AnnouncementState announcementState)
        {
            try
            {
                _logger.LogInformation($"Getting announcements by user ID {user.Id} from DB");

                // var user = await _dbContext.Users.FindAsync(userId);

                // if (user == null)
                // {
                //     throw new UserNotFoundException("User not found");
                // }

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
                    return Enumerable.Empty<AnnouncementResponseMiniDto>();
                }

                return _mapper.Map<List<AnnouncementResponseMiniDto>>(announcements);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting announcements for user with ID {user.Id}");
                throw;
            }
        }

        public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserFromDbAsync(ApplicationUser user,
            PagingParameters pagingParameters)
        {
            try
            {
                _logger.LogInformation($"Getting all announcements by user ID {user.Id} from DB");

                // var user = await _dbContext.Users.FindAsync(userId);

                // if (user == null)
                // {
                //     throw new UserNotFoundException("User not found");
                // }

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
                    return Enumerable.Empty<AnnouncementResponseMiniDto>();
                }

                return _mapper.Map<List<AnnouncementResponseMiniDto>>(announcement);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting all announcements for user with ID {user.Id}");
                throw;
            }
        }

        public async Task<IEnumerable<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsFromDbAsync(
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
                    return Enumerable.Empty<AnnouncementResponseMiniDto>();
                }

                return _mapper.Map<List<AnnouncementResponseMiniDto>>(filteredAnnouncements);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting filtered announcements");
                throw;
            }
        }
    }
}
using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Enums;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public AnnouncementRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private IEnumerable<Announcement> IncludeAllKeysInAnnouncement()
        {
            return _dbContext.Announcements.
                Include(x => x.Owner).
                Include(x => x.Vehicle).
                Include(x => x.Currency).
                Include(x => x.ImageUrls).
                Include(x => x.Vehicle.Year).
                Include(x => x.Vehicle.Make).
                Include(x => x.Vehicle.Model).
                Include(x => x.Vehicle.FuelType).
                Include(x => x.Vehicle.VehicleDetails).
                Include(x => x.Vehicle.VehicleDetails.BodyType).
                Include(x => x.Vehicle.VehicleDetails.DrivetrainType).
                Include(x => x.Vehicle.VehicleDetails.GearboxType).
                Include(x => x.Vehicle.VehicleDetails.Color).
                Include(x => x.Vehicle.VehicleDetails.MarketVersion).
                Include(x => x.Vehicle.VehicleDetails.Options).
                Include(x => x.Vehicle.VehicleDetails.Condition).
                Include(x => x.Country).
                Include(x => x.City);
        }

        public async Task<AnnouncementResponseDto> CreateAnnouncementAsync(Guid userId, CreateAnnouncementDto request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var result = await CheckAllRelationsInAnnouncement(request);

            if (result == false) return null;
            
            var announcement = new Announcement()
            {
                Vehicle = new Vehicle()
                {
                    Year = await _dbContext.Years.FindAsync(request.YearId),
                    Make = await _dbContext.Makes.FindAsync(request.MakeId),
                    Model = await _dbContext.Models.FindAsync(request.ModelId),
                    FuelType = await _dbContext.VehicleFuelTypes.FindAsync(request.FuelTypeId),
                    IsBrandNew = request.IsBrandNew,
                    
                    VehicleDetails = new VehicleDetails()
                    {
                        BodyType = await _dbContext.VehicleBodyTypes.FindAsync(request.BodyTypeId),
                        Color = await _dbContext.VehicleColors.FindAsync(request.ColorID),
                        HorsePower = request.HorsePower,
                        GearboxType = await _dbContext.VehicleGearboxTypes.FindAsync(request.GearboxId),
                        DrivetrainType = await _dbContext.VehicleDriveTrainTypes.FindAsync(request.DriveTrainTypeId),
                        MarketVersion = await _dbContext.VehicleMarketVersions.FindAsync(request.MarketVersionID),
                        OwnerQuantity = request.OwnerQuantity,
                        SeatCount = request.SeatCount,
                        VinCode = request.VinCode,
                        EngineVolume = request.EngineVolume,
                        MileAge = request.MileAge,
                        MileageType = request.MileageType
                    }
                },

                ExpirationDate = DateTimeOffset.Now.AddMonths(1),
                Barter = request.Barter,
                OnCredit = request.OnCredit,
                Description = request.Description,
                Price = request.Price,
                Currency = await _dbContext.Currencies.FindAsync(request.CurrencyId),
                Country = await _dbContext.Countries.FindAsync(request.CountryId),
                City = await _dbContext.Cities.FindAsync(request.CityId),
                Owner = user
            };
            
            user.Announcements.Add(announcement);
            var response = _dbContext.Announcements.Add(announcement).Entity;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AnnouncementResponseDto>(announcement);
        }
        

        private async Task<bool> CheckAllRelationsInAnnouncement(CreateAnnouncementDto request)
        {
            var model = await _dbContext.Models.FindAsync(request.ModelId);
            var make = await _dbContext.Makes.FindAsync(request.MakeId);
            var country = await _dbContext.Countries.FindAsync(request.CountryId);
            var city = await _dbContext.Cities.FindAsync(request.CityId);
            var currency = await _dbContext.Currencies.FindAsync(request.CurrencyId);
            var distanceUnit = request.MileageType;
            
            if (model.Make != make || country != city.Country || currency == null
                || distanceUnit != DistanceUnit.KM && distanceUnit != DistanceUnit.MI)
            {
                return false;
            }

            return true;
        }
        
        public AnnouncementResponseDto GetAnnouncementByIdFromDb(Guid id)
        {
            var response = IncludeAllKeysInAnnouncement().FirstOrDefault(x => x.Id == id);
            return _mapper.Map<AnnouncementResponseDto>(response);
        }

        public IEnumerable<AnnouncementResponseDto> GetAnnouncementsFromDb(PagingParameters parameter, AnnouncementState announcementState)
        {
            var announcements = IncludeAllKeysInAnnouncement().
                Where(on => on.AnnouncementState == announcementState).
                OrderBy(o => o.Price).
                Skip((parameter.PageNumber - 1) * parameter.PageSize).
                Take(parameter.PageSize).
                ToList();

            return _mapper.Map<List<AnnouncementResponseDto>>(announcements);
        }

        public async Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(Guid userId, Guid announcementId, CreateAnnouncementDto request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = GetAnnouncementByIdFromDb(announcementId);
            announcement.Vehicle.Year = await _dbContext.Years.FindAsync(request.YearId);
            announcement.Price = request.Price;
            await _dbContext.SaveChangesAsync();

            return announcement;
        }
        
        public async Task<AnnouncementResponseDto> ChangeAnnouncementStateInDbAsync(Guid userId, Guid announcementId, AnnouncementState announcementState)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = await _dbContext.Announcements.FirstOrDefaultAsync(x => x.Id == announcementId && x.Owner == user);

            if (announcement == null)
            {
                return null;
            }

            announcement.AnnouncementState = announcementState;
            announcement.ExpirationDate = DateTimeOffset.Now.AddMonths(1);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AnnouncementResponseDto>(announcement);
        }

        public async Task<AnnouncementResponseDto> DeleteInactiveAnnouncementFromDbAsync(Guid userId, Guid announcementId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = await _dbContext.Announcements.FirstOrDefaultAsync(x => x.Id == announcementId || x.AnnouncementState == AnnouncementState.Inactive || x.Owner.Id == userId);

            if (announcement == null)
            {
                return null;
            }

            var response = _dbContext.Announcements.Remove(announcement).Entity;

            return _mapper.Map<AnnouncementResponseDto>(response);
        }
        
        public async Task<IEnumerable<AnnouncementResponseDto>> GetFilteredAnnouncementsFromDbAsync(FilterParameters parameters)
        {
            var filteredAnnouncement = await _dbContext.Announcements
                .Where(x => x.Vehicle.Year.Id >= parameters.FromYearId 
                            && x.Vehicle.Year.Id <= parameters.ToYearId
                            && x.Vehicle.Make.Id == parameters.MakeId 
                            && x.Vehicle.Model.Id == parameters.ModelId 
                            && x.Vehicle.FuelType.Id == parameters.FuelTypeId 
                            && x.Vehicle.IsBrandNew == parameters.IsBrandNew
                            && x.Vehicle.VehicleDetails.BodyType.Id == parameters.BodyTypeId 
                            && x.Vehicle.VehicleDetails.Color.Id == parameters.ColorId 
                            && x.Vehicle.VehicleDetails.HorsePower >= parameters.FromHorsePower 
                            && x.Vehicle.VehicleDetails.HorsePower <= parameters.ToHorsePower 
                            && x.Vehicle.VehicleDetails.GearboxType.Id == parameters.GearboxTypeId 
                            && x.Vehicle.VehicleDetails.DrivetrainType.Id == parameters.DriveTrainTypeId
                            && x.Vehicle.VehicleDetails.MarketVersion.Id == parameters.MarketVersionId
                            && x.Vehicle.VehicleDetails.SeatCount == parameters.SeatCount
                            && x.Vehicle.VehicleDetails.EngineVolume == parameters.EngineVolume
                            && x.Vehicle.VehicleDetails.MileAge == parameters.Mileage
                            && x.Vehicle.VehicleDetails.MileageType == parameters.DistanceUnit
                            && x.Barter == parameters.Barter
                            && x.OnCredit == parameters.OnCredit
                            && x.Price >= parameters.FromPrice
                            && x.Price <= parameters.ToPrice
                            && x.Currency == parameters.Currency
                            && x.Country.Id == parameters.CountryId
                            && x.City.Id == parameters.CityId 
                ).ToListAsync();
            
            if (filteredAnnouncement == null)
            {
                return null;
            }
            
            return _mapper.Map<List<AnnouncementResponseDto>>(filteredAnnouncement);
        }
    }
}

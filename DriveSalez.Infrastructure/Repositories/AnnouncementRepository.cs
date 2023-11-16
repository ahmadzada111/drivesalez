﻿using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Enums;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        
        public AnnouncementRepository(ApplicationDbContext dbContext, IMapper mapper,
           IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<AnnouncementResponseDto> CreateAnnouncementAsync(Guid userId, CreateAnnouncementDto request)
        {
            var user = await _dbContext.Users.
                Include(x => x.PhoneNumbers).
                FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            if(!await CheckAllRelationsInAnnouncement(request)) return null;
            
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
                
                ImageUrls = await _fileService.UploadFilesAsync(request.ImageData),
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
            
            if (model.Make != make || country != city.Country || currency == null || distanceUnit != DistanceUnit.KM && distanceUnit != DistanceUnit.MI)
            {
                return false;
            }

            return true;
        }
        
        public async Task<AnnouncementResponseDto> GetAnnouncementByIdFromDb(Guid id)
        {
            var response = await _dbContext.Announcements.
                Include(x => x.Owner).
                Include(x => x.Owner.PhoneNumbers).
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
                Include(x => x.Vehicle.VehicleDetails.Conditions).
                Include(x => x.Country).
                Include(x => x.City).
                FirstOrDefaultAsync(x => x.Id == id);
            
            return _mapper.Map<AnnouncementResponseDto>(response);
        }

        public async Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsFromDb(PagingParameters parameter, AnnouncementState announcementState)
        {
            var announcements = await _dbContext.Announcements.
                Where(on => on.AnnouncementState == announcementState).
                Include(x => x.Owner).
                Include(x => x.Owner.PhoneNumbers).
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
                Include(x => x.Vehicle.VehicleDetails.Conditions).
                Include(x => x.Country).
                Include(x => x.City).
                OrderBy(o => o.Price).
                Skip((parameter.PageNumber - 1) * parameter.PageSize).
                Take(parameter.PageSize).
                ToListAsync();

            return _mapper.Map<List<AnnouncementResponseDto>>(announcements);
        }
        
        public async Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(Guid userId, Guid announcementId, UpdateAnnouncementDto request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var tmpAnnouncement = GetAnnouncementByIdFromDb(announcementId);
            var announcement = _mapper.Map<Announcement>(tmpAnnouncement);
            
            announcement.Id = announcementId;
            announcement.Vehicle.Year = await _dbContext.Years.FindAsync(request.YearId);
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

            _dbContext.Update(announcement);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AnnouncementResponseDto>(announcement);
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

        public async Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsByUserIdFromDbAsync(Guid userId, PagingParameters pagingParameters)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = await _dbContext.Announcements.
                Where(x => x.Owner.Id == userId).
                Include(x => x.Owner).
                Include(x => x.Owner.PhoneNumbers).
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
                Include(x => x.Vehicle.VehicleDetails.Conditions).
                Include(x => x.Country).
                Include(x => x.City).
                OrderBy(o => o.Price).
                Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).
                Take(pagingParameters.PageSize).
                ToListAsync();;

            if (announcement == null)
            {
                return null;
            }

            return _mapper.Map<List<AnnouncementResponseDto>>(announcement);
        }
        
       public async Task<IEnumerable<AnnouncementResponseDto>> GetFilteredAnnouncementsFromDbAsync(FilterParameters filterParameters, PagingParameters pagingParameters)
        {
            var filteredAnnouncement = await _dbContext.Announcements
                .Where(x => (x.Vehicle.Year.Id >= filterParameters.FromYearId
                             && x.Vehicle.Year.Id <= filterParameters.ToYearId) 
                            || x.Vehicle.Make.Id == filterParameters.MakeId 
                            || x.Vehicle.Model.Id == filterParameters.ModelId 
                            || x.Vehicle.FuelType.Id == filterParameters.FuelTypeId 
                            || x.Vehicle.IsBrandNew == filterParameters.IsBrandNew
                            || x.Vehicle.VehicleDetails.BodyType.Id == filterParameters.BodyTypeId 
                            || x.Vehicle.VehicleDetails.Color.Id == filterParameters.ColorId 
                            || (x.Vehicle.VehicleDetails.HorsePower >= filterParameters.FromHorsePower
                                && x.Vehicle.VehicleDetails.HorsePower <= filterParameters.ToHorsePower) 
                            || x.Vehicle.VehicleDetails.GearboxType.Id == filterParameters.GearboxTypeId 
                            || x.Vehicle.VehicleDetails.DrivetrainType.Id == filterParameters.DriveTrainTypeId
                            || x.Vehicle.VehicleDetails.MarketVersion.Id == filterParameters.MarketVersionId
                            || x.Vehicle.VehicleDetails.SeatCount == filterParameters.SeatCount
                            || x.Vehicle.VehicleDetails.EngineVolume == filterParameters.EngineVolume
                            || x.Vehicle.VehicleDetails.MileAge == filterParameters.Mileage
                            || x.Vehicle.VehicleDetails.MileageType == filterParameters.DistanceUnit
                            || x.Barter == filterParameters.Barter
                            || x.OnCredit == filterParameters.OnCredit
                            || x.Price >= filterParameters.FromPrice
                            || x.Price <= filterParameters.ToPrice
                            || x.Currency == filterParameters.Currency
                            || x.Country.Id == filterParameters.CountryId
                            || x.City.Id == filterParameters.CityId 
                ).
                Include(x => x.Owner).
                Include(x => x.Owner.PhoneNumbers).
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
                Include(x => x.Vehicle.VehicleDetails.Conditions).
                Include(x => x.Country).
                Include(x => x.City).
                OrderBy(o => o.Price).
                Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).
                Take(pagingParameters.PageSize).
                ToListAsync();
            
            filteredAnnouncement = filteredAnnouncement.
                Where(x =>
                    (filterParameters.OptionsIds == null || 
                     filterParameters.OptionsIds.All(optId => x.Vehicle?.VehicleDetails?.Options?.Any(opt => opt.Id == optId) == true)) &&
                    (filterParameters.ConditionsIds == null ||
                     filterParameters.ConditionsIds.All(condId => x.Vehicle?.VehicleDetails?.Conditions?.Any(cond => cond.Id == condId) == true))
                ).
                ToList();
            
            if (filteredAnnouncement == null)
            {
                return null;
            }
            
            return _mapper.Map<List<AnnouncementResponseDto>>(filteredAnnouncement);
        }
    }
}

﻿using System.Linq.Expressions;
using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
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

        public async Task<LimitRequestDto> GetUserLimitsFromDbAsync(Guid userId)
        {
            var user = await _dbContext.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return new LimitRequestDto()
            {
                PremiumLimit = user.PremiumUploadLimit,
                RegularLimit = user.RegularUploadLimit,
                AccountBalance = user.AccountBalance
            };
        }

        public async Task<AnnouncementResponseDto> CreateAnnouncementAsync(Guid userId, CreateAnnouncementDto request)
        {
            var user = await _dbContext.Users
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            if (!await CheckAllRelationsInAnnouncement(request)) return null;

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

            user.Announcements.Add(announcement);
            var response = await _dbContext.Announcements.AddAsync(announcement);

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<AnnouncementResponseDto>(announcement);
            }

            return null;
        }

        private async Task<bool> CheckAllRelationsInAnnouncement(CreateAnnouncementDto request)
        {
            var model = await _dbContext.Models.FindAsync(request.ModelId);
            var make = await _dbContext.Makes.FindAsync(request.MakeId);
            var country = await _dbContext.Countries.FindAsync(request.CountryId);
            var city = await _dbContext.Cities.FindAsync(request.CityId);
            var currency = await _dbContext.Currencies.FindAsync(request.CurrencyId);
            var distanceUnit = request.MileageType;

            if (model.Make != make || country != city.Country || currency == null ||
                distanceUnit != DistanceUnit.KM && distanceUnit != DistanceUnit.MI)
            {
                return false;
            }

            return true;
        }

        //CHECK!
        public async Task<AnnouncementResponseDto> GetAnnouncementByIdFromDb(Guid id)
        {
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
                .Include(x => x.Vehicle.VehicleDetails.BodyType)
                .Include(x => x.Vehicle.VehicleDetails.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails.Color)
                .Include(x => x.Vehicle.VehicleDetails.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails.Options)
                .Include(x => x.Vehicle.VehicleDetails.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (response == null)
            {
                throw new KeyNotFoundException();
            }

            response.ViewCount++;
            
            var result = _dbContext.Update(response);
            
            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<AnnouncementResponseDto>(response);
            }

            return null;
        }

        public async Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsFromDb(PagingParameters parameter,
            AnnouncementState announcementState)
        {
            var announcements = await _dbContext.Announcements
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
                .Include(x => x.Vehicle.VehicleDetails.BodyType)
                .Include(x => x.Vehicle.VehicleDetails.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails.Color)
                .Include(x => x.Vehicle.VehicleDetails.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails.Options)
                .Include(x => x.Vehicle.VehicleDetails.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            if (announcements == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<List<AnnouncementResponseDto>>(announcements);
        }

        public async Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(Guid userId, Guid announcementId,
            UpdateAnnouncementDto request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

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
                .Include(x => x.Vehicle.VehicleDetails.BodyType)
                .Include(x => x.Vehicle.VehicleDetails.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails.Color)
                .Include(x => x.Vehicle.VehicleDetails.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails.Options)
                .Include(x => x.Vehicle.VehicleDetails.Conditions)
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

            return null;
        }

        public async Task<AnnouncementResponseDto> MakeAnnouncementActiveInDbAsync(Guid userId, Guid announcementId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId && 
                                              x.Owner == user &&
                                              x.AnnouncementState != AnnouncementState.Active);

            if (announcement == null)
            {
                return null;
            }

            if (announcement.AnnouncementState == AnnouncementState.Active)
            {
                return null;
            }
            
            announcement.AnnouncementState = AnnouncementState.Active;
            announcement.ExpirationDate = DateTimeOffset.Now.AddMonths(1);

            var result = _dbContext.Announcements.Update(announcement);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<AnnouncementResponseDto>(announcement);
        }

        public async Task<AnnouncementResponseDto> MakeAnnouncementWaitingInDbAsync(Guid userId, Guid announcementId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var announcement =
                await _dbContext.Announcements
                    .FirstOrDefaultAsync(x => x.Id == announcementId && 
                                              x.Owner == user && 
                                              x.AnnouncementState != AnnouncementState.Waiting);

            if (announcement == null)
            {
                return null;
            }
            
            announcement.AnnouncementState = AnnouncementState.Waiting;

            var result = _dbContext.Announcements.Update(announcement);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
            }

            return _mapper.Map<AnnouncementResponseDto>(announcement);
        }
        
        public async Task<AnnouncementResponseDto> MakeAnnouncementInactiveInDbAsync(Guid userId, Guid announcementId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

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
            }

            return _mapper.Map<AnnouncementResponseDto>(announcement);
        }
        
        public async Task<AnnouncementResponseDto> DeleteInactiveAnnouncementFromDbAsync(Guid userId,
            Guid announcementId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var announcement = await _dbContext.Announcements
                .Where(x => x.Id == announcementId && 
                            x.AnnouncementState == AnnouncementState.Inactive && 
                            x.Owner.Id == userId)
                .FirstOrDefaultAsync();

            if (announcement == null)
            {
                return null;
            }

            var response = _dbContext.Announcements.Remove(announcement);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<AnnouncementResponseDto>(response);
            }

            return null;
        }

        public async Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsByUserIdFromDbAsync(Guid userId,
            PagingParameters pagingParameters, AnnouncementState announcementState)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var announcement = await _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.Owner.Id == userId && x.AnnouncementState == announcementState)
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
                .Include(x => x.Vehicle.VehicleDetails.BodyType)
                .Include(x => x.Vehicle.VehicleDetails.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails.Color)
                .Include(x => x.Vehicle.VehicleDetails.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails.Options)
                .Include(x => x.Vehicle.VehicleDetails.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();

            if (announcement == null)
            {
                return null;
            }

            return _mapper.Map<List<AnnouncementResponseDto>>(announcement);
        }

        public async Task<IEnumerable<AnnouncementResponseDto>> GetAllAnnouncementsByUserIdFromDbAsync(Guid userId,
            PagingParameters pagingParameters)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var announcement = await _dbContext.Announcements
                .AsNoTracking()
                .Where(x => x.Owner.Id == userId)
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
                .Include(x => x.Vehicle.VehicleDetails.BodyType)
                .Include(x => x.Vehicle.VehicleDetails.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails.Color)
                .Include(x => x.Vehicle.VehicleDetails.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails.Options)
                .Include(x => x.Vehicle.VehicleDetails.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();

            if (announcement == null)
            {
                return null;
            }

            return _mapper.Map<List<AnnouncementResponseDto>>(announcement);
        }

        public async Task<IEnumerable<AnnouncementResponseDto>> GetFilteredAnnouncementsFromDbAsync(
            FilterParameters filterParameters, PagingParameters pagingParameters)
        {
            var filteredAnnouncements = _dbContext.Announcements
                .AsNoTracking()
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
                .Include(x => x.Vehicle.VehicleDetails.BodyType)
                .Include(x => x.Vehicle.VehicleDetails.DrivetrainType)
                .Include(x => x.Vehicle.VehicleDetails.GearboxType)
                .Include(x => x.Vehicle.VehicleDetails.Color)
                .Include(x => x.Vehicle.VehicleDetails.MarketVersion)
                .Include(x => x.Vehicle.VehicleDetails.Options)
                .Include(x => x.Vehicle.VehicleDetails.Conditions)
                .Include(x => x.Country)
                .Include(x => x.City)
                .OrderBy(o => o.IsPremium)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize);
             
             if (filterParameters.FromYearId != null && filterParameters.ToYearId != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.Year.Id >= filterParameters.FromYearId && x.Vehicle.Year.Id <= filterParameters.ToYearId);
             }

             if (filterParameters.MakeId != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.Make.Id == filterParameters.MakeId);
             }
             
             if (filterParameters.IsBrandNew != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.IsBrandNew == filterParameters.IsBrandNew);
             }
             
             if (filterParameters.MakeId != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.Make.Id == filterParameters.MakeId);
             }
             
             if (filterParameters.FromHorsePower != null && filterParameters.ToHorsePower != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.VehicleDetails.HorsePower >= filterParameters.FromHorsePower 
                                                                          && x.Vehicle.VehicleDetails.HorsePower <= filterParameters.ToHorsePower);
             }
             
             if (filterParameters.SeatCount != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.VehicleDetails.SeatCount == filterParameters.SeatCount);
             }
             
             if (filterParameters.FromEngineVolume != null && filterParameters.ToEngineVolume != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.VehicleDetails.EngineVolume >= filterParameters.FromEngineVolume 
                                                                          && x.Vehicle.VehicleDetails.EngineVolume <= filterParameters.ToEngineVolume);
             }
             
             if (filterParameters.FromMileage != null && filterParameters.ToMileage != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.VehicleDetails.MileAge >= filterParameters.FromMileage
                                                                        && x.Vehicle.VehicleDetails.MileAge <= filterParameters.ToMileage);
             }
             
             if (filterParameters.MileageType != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Vehicle.VehicleDetails.MileageType == filterParameters.MileageType);
             }

             if (filterParameters.Barter != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Barter == filterParameters.Barter);
             }
             
             if (filterParameters.OnCredit != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.OnCredit == filterParameters.OnCredit);
             }
             
             if (filterParameters.FromPrice != null && filterParameters.ToPrice != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Price >= filterParameters.FromPrice
                                                                          && x.Price <= filterParameters.ToPrice);
             }
             
             if (filterParameters.OnCredit != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.OnCredit == filterParameters.OnCredit);
             }
             
             if (filterParameters.CurrencyId != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Currency.Id == filterParameters.CurrencyId);
             }
             
             if (filterParameters.CountryId != null)
             {
                 filteredAnnouncements = filteredAnnouncements.Where(x => x.Country.Id == filterParameters.CountryId);
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
                     .Where(x => filterParameters.DriveTrainTypesIds.Contains(x.Vehicle.VehicleDetails.DrivetrainType.Id));
             }
             
             if (filterParameters.MarketVersionsIds != null && filterParameters.MarketVersionsIds.Any())
             {
                 filteredAnnouncements = filteredAnnouncements
                     .Where(x => filterParameters.MarketVersionsIds.Contains(x.Vehicle.VehicleDetails.MarketVersion.Id));
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
             
             if (filteredAnnouncements == null)
             {
                 return null;
             }
            
             return _mapper.Map<List<AnnouncementResponseDto>>(filteredAnnouncements);
        }
    }
}

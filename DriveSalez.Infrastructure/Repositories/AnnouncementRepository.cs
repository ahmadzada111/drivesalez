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
    public class AnnouncementRepository : IAnnoucementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnnouncementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IEnumerable<Announcement> IncludeAllKeysInAnnouncement()
        {
            return _dbContext.Announcements.
                Include(x => x.Owner).
                Include(x => x.Vehicle).
                Include(x => x.Vehicle.Make).
                Include(x => x.Vehicle.Model).
                Include(x => x.Vehicle.ImageUrls).
                Include(x => x.Vehicle.FuelType).
                Include(x => x.Vehicle.VehicleDetails).
                Include(x => x.Vehicle.VehicleDetails.BodyType).
                Include(x => x.Vehicle.VehicleDetails.DriveTrainType).
                Include(x => x.Vehicle.VehicleDetails.GearboxType).
                Include(x => x.Vehicle.VehicleDetails.Color).
                Include(x => x.Vehicle.VehicleDetails.MarketVersion).
                Include(x => x.Vehicle.VehicleDetails.Options).
                Include(x => x.Vehicle.VehicleDetails.Condition).
                Include(x => x.Country).
                Include(x => x.City);
        }

        public async Task<Announcement> CreateAnnouncement(Guid userId, AnnouncementDto request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }


            var announcement = new Announcement()
            {
                Vehicle = new Vehicle()
                {
                    Year = request.Year,
                    Make = _dbContext.Makes.Find(request.MakeID),
                    Model = _dbContext.Models.Find(request.ModelID),
                    FuelType = _dbContext.VehicleFuelTypes.Find(request.FuelTypeID),
                    IsBrandNew = request.IsBrandNew,

                    VehicleDetails = new VehicleDetails()
                    {
                        BodyType = _dbContext.VehicleBodyTypes.Find(request.BodyTypeID),
                        Color = _dbContext.VehicleColors.Find(request.ColorID),
                        HorsePower = request.HorsePower,
                        GearboxType = _dbContext.VehicleGearboxTypes.Find(request.GearboxID),
                        DriveTrainType = _dbContext.VehicleDriveTrainTypes.Find(request.DriveTrainTypeID),
                        //Condition=_dbContext.Vehicle
                        MarketVersion = _dbContext.VehicleMarketVersions.Find(request.MarketVersionID),
                        OwnerQuantity = request.OwnerQuantity,
                        SeatCount = request.SeatCount,
                        VinCode = request.VinCode,
                        EngineVolume = request.EngineVolume,
                        MileAge = request.MileAge,
                        MileageType = request.MileageType
                    }

                },

                Barter = request.Barter,
                OnCredit = request.OnCredit,
                Description = request.Description,
                Price = request.Price,
                Currency = request.Currency,
                Country = _dbContext.Countries.Find(request.Country.Id),
                City = _dbContext.Cities.Find(request.City.Id),
                Owner = user
            };


            if (announcement.Vehicle.Model.Make != announcement.Vehicle.Make)
            {
                return null;
            }

            user.Announcements.Add(announcement);
            var response = _dbContext.Announcements.Add(announcement).Entity;

            _dbContext.SaveChanges();

            return response;
        }

        public async Task<Announcement> GetAnnouncementByIdFromDb(int id)
        {
            var response = IncludeAllKeysInAnnouncement().FirstOrDefault(x => x.Id == id);
            return response;
        }

        public Announcement ConvertToAnnouncement(AnnouncementDto request)
        {
            var announcement = new Announcement()
            {
                Vehicle = new Vehicle()
                {
                    Year = request.Year,
                    Make = _dbContext.Makes.Find(request.MakeID),
                    Model = _dbContext.Models.Find(request.ModelID),
                    FuelType = _dbContext.VehicleFuelTypes.Find(request.FuelTypeID),
                    IsBrandNew = request.IsBrandNew,

                    VehicleDetails = new VehicleDetails()
                    {
                        BodyType = _dbContext.VehicleBodyTypes.Find(request.BodyTypeID),
                        Color = _dbContext.VehicleColors.Find(request.ColorID),
                        HorsePower = request.HorsePower,
                        GearboxType = _dbContext.VehicleGearboxTypes.Find(request.GearboxID),
                        DriveTrainType = _dbContext.VehicleDriveTrainTypes.Find(request.DriveTrainTypeID),
                        //Condition=_dbContext.Vehicle
                        MarketVersion = _dbContext.VehicleMarketVersions.Find(request.MarketVersionID),
                        OwnerQuantity = request.OwnerQuantity,
                        SeatCount = request.SeatCount,
                        VinCode = request.VinCode,
                        EngineVolume = request.EngineVolume,
                        MileAge = request.MileAge,
                        MileageType = request.MileageType
                    }
                },

                Barter = request.Barter,
                OnCredit = request.OnCredit,
                Description = request.Description,
                Price = request.Price,
                Currency = request.Currency,
                City = request.City,
                Country = request.Country,
                Owner = _dbContext.Users.Find(request.ApplicationUserID)
            };

            return announcement;
        }

        public IEnumerable<Announcement> GetAnnouncementsFromDb(PagingParameters parameter, AnnouncementState announcementState)
        {
            return IncludeAllKeysInAnnouncement().
                Where(on => on.AnnoucementState == announcementState).
                OrderBy(o => o.Price).
                Skip((parameter.PageNumber - 1) * parameter.PageSize).
                Take(parameter.PageSize).
                ToList();
        }

        public async Task<Announcement> UpdateAnnouncementInDb(Guid userId, int announcementId, AnnouncementDto request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = await GetAnnouncementByIdFromDb(announcementId);
            announcement.Vehicle.Year = request.Year;
            announcement.Price = request.Price;
            _dbContext.SaveChanges();

            return announcement;
        }

        public async Task<Announcement> ChangeAnnouncementStateInDb(Guid userId, int announcementId, AnnouncementState announcementState)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = _dbContext.Announcements.FirstOrDefault(x => x.Id == announcementId);

            if (announcement == null)
            {
                return null;
            }

            announcement.AnnoucementState = announcementState;

            _dbContext.SaveChanges();

            return announcement;
        }

        public async Task<Announcement> DeleteInactiveAnnouncementFromDb(Guid userId, int announcementId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = _dbContext.Announcements.FirstOrDefault(x => x.Id == announcementId || x.AnnoucementState == AnnouncementState.Inactive || x.Owner.Id == userId);

            if (announcement == null)
            {
                return null;
            }

            var response = _dbContext.Announcements.Remove(announcement).Entity;

            return response;
        }
    }
}

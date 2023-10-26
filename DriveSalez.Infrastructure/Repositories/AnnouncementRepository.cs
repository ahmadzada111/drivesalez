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

        public AnnouncementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IEnumerable<Announcement> IncludeAllKeysInAnnouncement()
        {
            return _dbContext.Announcements.
                Include(x => x.Owner).
                Include(x => x.Vehicle).
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

                Barter = request.Barter,
                OnCredit = request.OnCredit,
                Description = request.Description,
                Price = request.Price,
                Currency = request.Currency,
                Country = await _dbContext.Countries.FindAsync(request.CountryId),
                City = await _dbContext.Cities.FindAsync(request.CityId),
                Owner = user
            };

            if (announcement.Vehicle.Model.Make != announcement.Vehicle.Make)
            {
                return null;
            }

            user.Announcements.Add(announcement);
            var response = _dbContext.Announcements.Add(announcement).Entity;

            await _dbContext.SaveChangesAsync();

            return response;
        }

        public Announcement GetAnnouncementByIdFromDb(int id)
        {
            var response = IncludeAllKeysInAnnouncement().FirstOrDefault(x => x.Id == id);
            return response;
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

            var announcement = GetAnnouncementByIdFromDb(announcementId);
            announcement.Vehicle.Year = await _dbContext.Years.FindAsync(request.YearId);
            announcement.Price = request.Price;
            await _dbContext.SaveChangesAsync();

            return announcement;
        }

        public async Task<Announcement> ChangeAnnouncementStateInDb(Guid userId, int announcementId, AnnouncementState announcementState)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            var announcement = _dbContext.Announcements.FirstOrDefault(x => x.Id == announcementId && x.Owner == user);

            if (announcement == null)
            {
                return null;
            }

            announcement.AnnoucementState = announcementState;

            await _dbContext.SaveChangesAsync();

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

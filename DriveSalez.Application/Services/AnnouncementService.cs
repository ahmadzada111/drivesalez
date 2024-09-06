using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

internal sealed class AnnouncementService : IAnnouncementService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public AnnouncementService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, 
        IFileService fileService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _fileService = fileService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    private async Task<Announcement> MapUpdateAnnouncementDtoToModelAsync(UpdateAnnouncementDto request, UserProfile user)
    {
        var year = await _unitOfWork.ManufactureYears.FindById(request.YearId);
        var fuelType = await _unitOfWork.FuelTypes.FindById(request.FuelTypeId);
        var isBrandNew = request.IsBrandNew;
        var bodyType = await _unitOfWork.BodyTypes.FindById(request.BodyTypeId);
        var color = await _unitOfWork.Colors.FindById(request.ColorId);
        var horsePower = request.HorsePower;
        var gearboxType = await _unitOfWork.GearboxTypes.FindById(request.GearboxId);
        var drivetrainType = await _unitOfWork.DrivetrainTypes.FindById(request.DrivetrainTypeId);
        var marketVersion = await _unitOfWork.MarketVersions.FindById(request.MarketVersionId);
        var ownerQuantity = request.OwnerQuantity;
        var options = await _unitOfWork.Options.FindAll(e => request.OptionsIds.Contains(e.Id));
        var conditions = await _unitOfWork.Conditions.FindAll(e => request.ConditionsIds.Contains(e.Id));
        var seatCount = request.SeatCount;
        var vinCode = request.VinCode;
        var engineVolume = request.EngineVolume;
        var mileage = request.Mileage;
        var mileageType = request.DistanceUnit;

        var announcement = new Announcement
        {
            Vehicle = new Vehicle
            {
                Make = await _unitOfWork.Makes.FindById(request.MakeId),
                Model = await _unitOfWork.Models.FindById(request.ModelId),
                VehicleDetail = new VehicleDetail(year, fuelType, isBrandNew, bodyType, color, horsePower, gearboxType, drivetrainType,
                marketVersion, ownerQuantity, options.ToList(), conditions.ToList(), seatCount, vinCode, engineVolume, mileage, Enum.Parse<DistanceUnit>(mileageType))
            },
            Barter = request.Barter,
            OnCredit = request.OnCredit,
            Description = request.Description,
            Price = request.Price,
            Country = await _unitOfWork.Countries.FindById(request.CountryId),
            City = await _unitOfWork.Cities.FindById(request.CityId),
            Owner = user
        };

        return announcement;
    }

    private async Task<Announcement> MapCreateAnnouncementDtoToModelAsync(CreateAnnouncementDto request, UserProfile user)
    {
        var year = await _unitOfWork.ManufactureYears.FindById(request.YearId);
        var fuelType = await _unitOfWork.FuelTypes.FindById(request.FuelTypeId);
        var isBrandNew = request.IsBrandNew;
        var bodyType = await _unitOfWork.BodyTypes.FindById(request.BodyTypeId);
        var color = await _unitOfWork.Colors.FindById(request.ColorId);
        var horsePower = request.HorsePower;
        var gearboxType = await _unitOfWork.GearboxTypes.FindById(request.GearboxId);
        var drivetrainType = await _unitOfWork.DrivetrainTypes.FindById(request.DrivetrainTypeId);
        var marketVersion = await _unitOfWork.MarketVersions.FindById(request.MarketVersionId);
        var ownerQuantity = request.OwnerQuantity;
        var options = await _unitOfWork.Options.FindAll(e => request.OptionsIds.Contains(e.Id));
        var conditions = await _unitOfWork.Conditions.FindAll(e => request.ConditionsIds.Contains(e.Id));
        var seatCount = request.SeatCount;
        var vinCode = request.VinCode;
        var engineVolume = request.EngineVolume;
        var mileage = request.Mileage;
        var mileageType = request.DistanceUnit;

        var announcement = new Announcement()
                {
                    Vehicle = new Vehicle()
                    {
                        Make = await _unitOfWork.Makes.FindById(request.MakeId),
                        Model = await _unitOfWork.Models.FindById(request.ModelId),
                        VehicleDetail = new VehicleDetail(year, fuelType, isBrandNew.GetValueOrDefault(), bodyType, color, horsePower, gearboxType, drivetrainType,
                        marketVersion, ownerQuantity.GetValueOrDefault(), options.ToList(), conditions.ToList(), seatCount.GetValueOrDefault(), 
                        vinCode, engineVolume.GetValueOrDefault(), mileage, Enum.Parse<DistanceUnit>(mileageType))
                    },
                    ExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Barter = request.Barter,
                    OnCredit = request.OnCredit,
                    Description = request.Description,
                    Price = request.Price,
                    Country = await _unitOfWork.Countries.FindById(request.CountryId),
                    City = await _unitOfWork.Cities.FindById(request.CityId),
                    IsPremium = request.IsPremium,
                    PremiumExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Owner = user
                };

        return announcement;
    }
    
    private async Task<bool> CheckAllRelationsInAnnouncementAsync(Announcement request)
    {
        var model = await _unitOfWork.Models
            .Find(m => m.Id == request.Vehicle.Model.Id, 
                m => m.Make);

        var city = await _unitOfWork.Cities
            .Find(c => c.Id == request.City.Id,
                c => c.Country);
        
        return model?.Make.Id == request.Vehicle.Make.Id && city?.Country.Id == request.Country.Id;
    }
    
    // private bool UpdateUserUploadLimits(bool isPremium, ApplicationUser user)
    // {
    //     if (isPremium)
    //     {
    //         if (user.PremiumUploadLimit <= 0)
    //         {
    //             return false;
    //         }
    //
    //         user.PremiumUploadLimit--;
    //     }
    //     else if (user.RegularUploadLimit > 0)
    //     {
    //         user.RegularUploadLimit--;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    //
    //     return true;
    // }
    
    public async Task<GetAnnouncementDto> CreateAnnouncement(CreateAnnouncementDto request)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var identityUser = await _userManager.GetUserAsync(httpContext.User) 
                   ?? throw new UserNotAuthorizedException("User is not authorized!");
        var baseUser = await _unitOfWork.Users.FindUserOfType<UserProfile>(identityUser.BaseUser.Id) 
                       ?? throw new UserNotFoundException("User not found!");
        // if (!UpdateUserUploadLimits(request.IsPremium, user))
        // {
        //     throw new InvalidOperationException("User has insufficient limit to perform this action!");
        // }

        var announcement = await MapCreateAnnouncementDtoToModelAsync(request, baseUser);
        
        if (!await CheckAllRelationsInAnnouncementAsync(announcement))
        {
            throw new ValidationException("Invalid relationships in the announcement.");
        }
        
        var urls = await _fileService.UploadFilesAsync(request.ImageData, baseUser);
        announcement.ImageUrls.ToList().AddRange(urls); 
        var response = _unitOfWork.Announcements.Add(announcement);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<GetAnnouncementDto>(response);
    }
    
    public async Task<PaginatedList<GetAnnouncementMiniDto>> GetAllAnnouncements(Expression<Func<Announcement, bool>>? whereExpression, PagingParameters pagingParameters)
    {
        var announcements = await _unitOfWork.Announcements.FindAll(whereExpression,
            x => x.Owner,
            x => x.Owner.PhoneNumbers ?? new List<PhoneNumber>(),
            x => x.Vehicle,
            x => x.ImageUrls,
            x => x.Vehicle.Make,
            x => x.Vehicle.Model,
            x => x.Vehicle.VehicleDetail.Year,
            x => x.Vehicle.VehicleDetail.FuelType,
            x => x.Vehicle.VehicleDetail.DrivetrainType,
            x => x.Vehicle.VehicleDetail.BodyType,
            x => x.Vehicle.VehicleDetail.GearboxType,
            x => x.Vehicle.VehicleDetail.Color,
            x => x.Vehicle.VehicleDetail.MarketVersion,
            x => x.Vehicle.VehicleDetail.Options,
            x => x.Vehicle.VehicleDetail.Conditions,
            x => x.Country,
            x => x.City
        );

        var enumerable = announcements.ToList();
        var paginatedAnnouncements = PaginatedList<Announcement>.ToPaginatedList(enumerable,
            pagingParameters.PageIndex, pagingParameters.PageSize, enumerable.Count());
        
        return _mapper.Map<PaginatedList<GetAnnouncementMiniDto>>(paginatedAnnouncements);
    }

    public async Task<GetAnnouncementDto> FindAnnouncementById(Guid id, bool incrementViewCount = false)
    {
        var announcement = await _unitOfWork.Announcements.Find(x => x.Id == id,
            x => x.Owner,
            x => x.Owner.PhoneNumbers ?? new List<PhoneNumber>(),
            x => x.Vehicle,
            x => x.ImageUrls,
            x => x.Vehicle.Make,
            x => x.Vehicle.Model,
            x => x.Vehicle.VehicleDetail.Year,
            x => x.Vehicle.VehicleDetail.FuelType,
            x => x.Vehicle.VehicleDetail.DrivetrainType,
            x => x.Vehicle.VehicleDetail.BodyType,
            x => x.Vehicle.VehicleDetail.GearboxType,
            x => x.Vehicle.VehicleDetail.Color,
            x => x.Vehicle.VehicleDetail.MarketVersion,
            x => x.Vehicle.VehicleDetail.Options,
            x => x.Vehicle.VehicleDetail.Conditions,
            x => x.Country,
            x => x.City
        );

        if (announcement is not null && incrementViewCount)
        {
            announcement.ViewCount++;
            _unitOfWork.Announcements.Update(announcement);
            await _unitOfWork.SaveChangesAsync();
        }
        
        return _mapper.Map<GetAnnouncementDto>(announcement);
    }

    public async Task<bool> DeleteAnnouncement(Guid announcementId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var announcementToDelete = await _unitOfWork.Announcements.Find(x => x.Id == announcementId,
            x => x.Owner);

        if (announcementToDelete is null || announcementToDelete.AnnouncementState != AnnouncementState.Inactive
                                         || announcementToDelete.Owner.Id != user.BaseUser.Id) return false;
        
        _unitOfWork.Announcements.Delete(announcementToDelete);
        await _fileService.DeleteAllFilesAsync(user.Id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<GetAnnouncementDto?> ChangeAnnouncementState(Guid announcementId, AnnouncementState announcementState)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var announcement = await _unitOfWork.Announcements.Find(x => x.Id == announcementId,
            x => x.Owner);

        if (announcement is not null && announcement.AnnouncementState != announcementState &&
            announcement.Owner.Id == user.Id)
        {
            if(announcement.ExpirationDate < DateTime.UtcNow)
            {
                announcement.ExpirationDate = DateTimeOffset.Now.AddMonths(1);
            }
            
            announcement.AnnouncementState = announcementState;
            _unitOfWork.Announcements.Update(announcement);
            await _unitOfWork.SaveChangesAsync();
        }
        
        return _mapper.Map<GetAnnouncementDto>(announcement);
    }
    
    public async Task<Tuple<IEnumerable<GetAnnouncementMiniDto>, PaginatedList<GetAnnouncementMiniDto>>> GetAllActiveAnnouncements(PagingParameters pagingParameters)
    {
        var response = await _unitOfWork.Announcements.GetAllActiveAnnouncementsFromDbAsync(pagingParameters);
        return _mapper.Map<Tuple<IEnumerable<GetAnnouncementMiniDto>, PaginatedList<GetAnnouncementMiniDto>>>(response);
    }
    
    public async Task<GetAnnouncementDto> UpdateAnnouncement(UpdateAnnouncementDto announcementDto, Guid announcementId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var identityUser = await _userManager.GetUserAsync(httpContext.User) 
                           ?? throw new UserNotAuthorizedException("User is not authorized!");
        var baseUser = await _unitOfWork.Users.FindUserOfType<UserProfile>(identityUser.BaseUser.Id) 
                       ?? throw new UserNotFoundException("User not found!");
        var updateData = await MapUpdateAnnouncementDtoToModelAsync(announcementDto, baseUser);
        var announcementToUpdate = await _unitOfWork.Announcements.Find(x => x.Id == announcementId,
            x => x.Owner,
            x => x.Owner.PhoneNumbers ?? new List<PhoneNumber>(),
            x => x.Vehicle,
            x => x.ImageUrls,
            x => x.Vehicle.Make,
            x => x.Vehicle.Model,
            x => x.Vehicle.VehicleDetail.Year,
            x => x.Vehicle.VehicleDetail.FuelType,
            x => x.Vehicle.VehicleDetail.DrivetrainType,
            x => x.Vehicle.VehicleDetail.BodyType,
            x => x.Vehicle.VehicleDetail.GearboxType,
            x => x.Vehicle.VehicleDetail.Color,
            x => x.Vehicle.VehicleDetail.MarketVersion,
            x => x.Vehicle.VehicleDetail.Options,
            x => x.Vehicle.VehicleDetail.Conditions,
            x => x.Country,
            x => x.City
        );
        
        announcementToUpdate = _mapper.Map(updateData, announcementToUpdate);
        
        if (!await CheckAllRelationsInAnnouncementAsync(announcementToUpdate))
        {
            throw new ValidationException("Invalid relationships in the announcement.");
        }
        
        var updatedAnnouncement = _unitOfWork.Announcements.Update(announcementToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<GetAnnouncementDto>(updatedAnnouncement);
    }

    public async Task<PaginatedList<GetAnnouncementMiniDto>> GetFilteredAnnouncementsAsync(FilterAnnouncementParameters filterAnnouncementParameters, PagingParameters pagingParameters)
    {
        var response = await _unitOfWork.Announcements.GetFilteredAnnouncementsFromDbAsync(filterAnnouncementParameters, pagingParameters);
        return _mapper.Map<PaginatedList<GetAnnouncementMiniDto>>(response);
    }
}
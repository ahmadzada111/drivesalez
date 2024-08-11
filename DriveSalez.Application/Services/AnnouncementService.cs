using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public AnnouncementService(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager, 
        IAnnouncementRepository announcementRepository, IFileService fileService, IMapper mapper)
    {
        _contextAccessor = accessor;
        _userManager = userManager;
        _announcementRepository = announcementRepository;
        _fileService = fileService;
        _mapper = mapper;
    }
    
    private async Task<Announcement> MapUpdateAnnouncementDtoToModelAsync(UpdateAnnouncementDto request, ApplicationUser user)
    {
        var year = await _announcementRepository.GetManufactureYearById(request.YearId);
        var fuelType = await _announcementRepository.GetFuelTypeById(request.FuelTypeId);
        var isBrandNew = request.IsBrandNew;
        var bodyType = await _announcementRepository.GetBodyTypeById(request.BodyTypeId);
        var color = await _announcementRepository.GetColorById(request.ColorId);
        var horsePower = request.HorsePower;
        var gearboxType = await _announcementRepository.GetGearboxById(request.GearboxId);
        var drivetrainType = await _announcementRepository.GetDrivetrainTypeById(request.DrivetrainTypeId);
        var marketVersion = await _announcementRepository.GetMarketVersionById(request.MarketVersionId);
        var ownerQuantity = request.OwnerQuantity;
        var options = await _announcementRepository.GetOptionsByIds(request.OptionsIds);
        var conditions = await _announcementRepository.GetConditionsByIds(request.ConditionsIds);
        var seatCount = request.SeatCount;
        var vinCode = request.VinCode;
        var engineVolume = request.EngineVolume;
        var mileage = request.Mileage;
        var mileageType = request.MileageType;

        var announcement = new Announcement
        {
            Vehicle = new Vehicle
            {
                Make = await _announcementRepository.GetMakeById(request.MakeId),
                Model = await _announcementRepository.GetModelById(request.ModelId),
                VehicleDetail = new VehicleDetail(year, fuelType, isBrandNew, bodyType, color, horsePower, gearboxType, drivetrainType,
                marketVersion, ownerQuantity, options, conditions, seatCount, vinCode, engineVolume, mileage, Enum.Parse<DistanceUnit>(mileageType))
            },
            Barter = request.Barter,
            OnCredit = request.OnCredit,
            Description = request.Description,
            Price = request.Price,
            Country = await _announcementRepository.GetCountryById(request.CountryId),
            City = await _announcementRepository.GetCityById(request.CityId),
            Owner = user
        };

        return announcement;
    }

    private async Task<Announcement> MapCreateAnnouncementDtoToModelAsync(CreateAnnouncementDto request, ApplicationUser user)
    {
        var year = await _announcementRepository.GetManufactureYearById(request.YearId);
        var fuelType = await _announcementRepository.GetFuelTypeById(request.FuelTypeId);
        var isBrandNew = request.IsBrandNew;
        var bodyType = await _announcementRepository.GetBodyTypeById(request.BodyTypeId);
        var color = await _announcementRepository.GetColorById(request.ColorId);
        var horsePower = request.HorsePower;
        var gearboxType = await _announcementRepository.GetGearboxById(request.GearboxId);
        var drivetrainType = await _announcementRepository.GetDrivetrainTypeById(request.DrivetrainTypeId);
        var marketVersion = await _announcementRepository.GetMarketVersionById(request.MarketVersionId);
        var ownerQuantity = request.OwnerQuantity;
        var options = await _announcementRepository.GetOptionsByIds(request.OptionsIds);
        var conditions = await _announcementRepository.GetConditionsByIds(request.ConditionsIds);
        var seatCount = request.SeatCount;
        var vinCode = request.VinCode;
        var engineVolume = request.EngineVolume;
        var mileage = request.Mileage;
        var mileageType = request.MileageType;

        var announcement = new Announcement()
                {
                    Vehicle = new Vehicle()
                    {
                        Make = await _announcementRepository.GetMakeById(request.MakeId),
                        Model = await _announcementRepository.GetModelById(request.ModelId),
                        VehicleDetail = new VehicleDetail(year, fuelType, isBrandNew.GetValueOrDefault(), bodyType, color, horsePower, gearboxType, drivetrainType,
                        marketVersion, ownerQuantity.GetValueOrDefault(), options, conditions, seatCount.GetValueOrDefault(), 
                        vinCode, engineVolume.GetValueOrDefault(), mileage, Enum.Parse<DistanceUnit>(mileageType))
                    },
                    ExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Barter = request.Barter,
                    OnCredit = request.OnCredit,
                    Description = request.Description,
                    Price = request.Price,
                    Country = await _announcementRepository.GetCountryById(request.CountryId),
                    City = await _announcementRepository.GetCityById(request.CityId),
                    IsPremium = request.IsPremium,
                    PremiumExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Owner = user
                };

        return announcement;
    }
    
    private bool UpdateUserUploadLimits(bool isPremium, ApplicationUser user)
    {
        if (isPremium)
        {
            if (user.PremiumUploadLimit <= 0)
            {
                return false;
            }

            user.PremiumUploadLimit--;
        }
        else if (user.RegularUploadLimit > 0)
        {
            user.RegularUploadLimit--;
        }
        else
        {
            return false;
        }

        return true;
    }
    
    public async Task<AnnouncementResponseDto?> CreateAnnouncementAsync(CreateAnnouncementDto request)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");

        // if (!UpdateUserUploadLimits(request.IsPremium, user))
        // {
        //     return null;
        // }

        var announcement = await MapCreateAnnouncementDtoToModelAsync(request, user);
        
        if (!await _announcementRepository.CheckAllRelationsInAnnouncement(announcement))
        {
            throw new ValidationException("Invalid relationships in the announcement.");
        }
        
        var urls = await _fileService.UploadFilesAsync(request.ImageData, user);
        announcement.ImageUrls.AddRange(urls); 

        var response = await _announcementRepository.CreateAnnouncementInDbAsync(user, announcement);
        
        return _mapper.Map<AnnouncementResponseDto>(response);
    }
    
    public async Task<AnnouncementResponseDto?> DeleteInactivateAnnouncementAsync(Guid announcementId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var response = await _announcementRepository.DeleteInactiveAnnouncementFromDbAsync(user, announcementId);

        await _fileService.DeleteAllFilesAsync(user.Id);
        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllPremiumAnnouncementsAsync(PagingParameters pagingParameters)
    {
        var response = await _announcementRepository.GetAllPremiumAnnouncementsFromDbAsync(pagingParameters);
        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }

    public async Task<LimitRequestDto> GetUserLimitsAsync()
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        
        return new LimitRequestDto()
        {
            PremiumLimit = user.PremiumUploadLimit,
            RegularLimit = user.RegularUploadLimit,
            AccountBalance = user.AccountBalance
        };
    }
    
    public async Task<AnnouncementResponseDto?> GetAnnouncementByIdAsync(Guid id)
    {
        var response = await _announcementRepository.GetAnnouncementByIdFromDbAsync(id);
        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdAsync(Guid id)
    {
        var response = await _announcementRepository.GetActiveAnnouncementByIdFromDbAsync(id);
        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<Tuple<IEnumerable<AnnouncementResponseMiniDto>, PaginatedList<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncements(PagingParameters parameters)
    {
        var response = await _announcementRepository.GetAllActiveAnnouncementsFromDbAsync(parameters);
        return _mapper.Map<Tuple<IEnumerable<AnnouncementResponseMiniDto>, PaginatedList<AnnouncementResponseMiniDto>>>(response);
    }

    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllAnnouncementsForAdminPanelAsync(
        PagingParameters parameters, AnnouncementState announcementState)
    {
        var response = await _announcementRepository.GetAllAnnouncementsForAdminPanelFromDbAsync(parameters, announcementState);
        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }
    
    public async Task<AnnouncementResponseDto?> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var announcement = await MapUpdateAnnouncementDtoToModelAsync(request, user);
        
        if (!await _announcementRepository.CheckAllRelationsInAnnouncement(announcement))
        {
            throw new ValidationException("Invalid relationships in the announcement.");
        }
        
        var updatedAnnouncement = await _announcementRepository.UpdateAnnouncementInDbAsync(user, announcementId, announcement);

        return _mapper.Map<AnnouncementResponseDto>(updatedAnnouncement);
    }

    public async Task<AnnouncementResponseDto?> ChangeAnnouncementState(Guid announcementId, AnnouncementState announcementState)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var response = await _announcementRepository.ChangeAnnouncementStateInDbAsync(user, announcementId, announcementState);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }

    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        var response = await _announcementRepository.GetFilteredAnnouncementsFromDbAsync(filterParameters, pagingParameters);
        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }

    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserAsync(PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var response = await _announcementRepository.GetAnnouncementsByStatesAndByUserFromDbAsync(user, pagingParameters, announcementState);

        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }
    
    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserAsync(PagingParameters pagingParameters)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not authorized!");
        var response = await _announcementRepository.GetAllAnnouncementsByUserFromDbAsync(user, pagingParameters);

        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }
}
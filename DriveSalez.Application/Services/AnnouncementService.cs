using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.DTO.AnnoucementDTO;
using DriveSalez.Application.DTO.AnnouncementDTO;
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
        var announcement = new Announcement
        {
            Vehicle = new Vehicle
            {
                Year = await _announcementRepository.GetManufactureYearById(request.YearId.Value),
                Make = await _announcementRepository.GetMakeById(request.MakeId.Value),
                Model = await _announcementRepository.GetModelById(request.ModelId.Value),
                FuelType = await _announcementRepository.GetFuelTypeById(request.FuelTypeId.Value),
                IsBrandNew = request.IsBrandNew,

                VehicleDetails = new VehicleDetails
                {
                    BodyType = await _announcementRepository.GetBodyTypeById(request.BodyTypeId.Value),
                    Color = await _announcementRepository.GetColorById(request.ColorId.Value),
                    HorsePower = request.HorsePower,
                    GearboxType = await _announcementRepository.GetGearboxById(request.GearboxId.Value),
                    DrivetrainType = await _announcementRepository.GetDrivetrainTypeById(request.DrivetrainTypeId.Value),
                    MarketVersion = await _announcementRepository.GetMarketVersionById(request.MarketVersionId.Value),
                    OwnerQuantity = request.OwnerQuantity,
                    Options = await _announcementRepository.GetOptionsByIds(request.OptionsIds),
                    Conditions = await _announcementRepository.GetConditionsByIds(request.ConditionsIds),
                    SeatCount = request.SeatCount,
                    VinCode = request.VinCode,
                    EngineVolume = request.EngineVolume,
                    MileAge = request.Mileage,
                    MileageType = Enum.Parse<DistanceUnit>(request.MileageType, true)
                }
            },
            Barter = request.Barter,
            OnCredit = request.OnCredit,
            Description = request.Description,
            Price = request.Price,
            Currency = await _announcementRepository.GetCurrencyById(request.CurrencyId),
            Country = await _announcementRepository.GetCountryById(request.CountryId),
            City = await _announcementRepository.GetCityById(request.CityId),
            Owner = user
        };

        return announcement;
    }

    private async Task<Announcement> MapCreateAnnouncementDtoToModelAsync(CreateAnnouncementDto request, ApplicationUser user)
    {
        var announcement = new Announcement()
                {
                    Vehicle = new Vehicle()
                    {
                        Year = await _announcementRepository.GetManufactureYearById(request.YearId.Value),
                        Make = await _announcementRepository.GetMakeById(request.MakeId.Value),
                        Model = await _announcementRepository.GetModelById(request.ModelId.Value),
                        FuelType = await _announcementRepository.GetFuelTypeById(request.FuelTypeId.Value),
                        IsBrandNew = request.IsBrandNew,

                        VehicleDetails = new VehicleDetails()
                        {
                            BodyType = await _announcementRepository.GetBodyTypeById(request.BodyTypeId.Value),
                            Color = await _announcementRepository.GetColorById(request.ColorId.Value),
                            HorsePower = request.HorsePower,
                            GearboxType = await _announcementRepository.GetGearboxById(request.GearboxId.Value),
                            DrivetrainType = await _announcementRepository.GetDrivetrainTypeById(request.DrivetrainTypeId.Value),
                            MarketVersion = await _announcementRepository.GetMarketVersionById(request.MarketVersionId.Value),
                            OwnerQuantity = request.OwnerQuantity,
                            Options = await _announcementRepository.GetOptionsByIds(request.OptionsIds),
                            Conditions = await _announcementRepository.GetConditionsByIds(request.ConditionsIds),
                            SeatCount = request.SeatCount,
                            VinCode = request.VinCode,
                            EngineVolume = request.EngineVolume,
                            MileAge = request.Mileage,
                            MileageType = request.MileageType
                        }
                    },
                    ViewCount = 0,
                    ExpirationDate = DateTimeOffset.Now.AddMonths(1),
                    Barter = request.Barter,
                    OnCredit = request.OnCredit,
                    Description = request.Description,
                    Price = request.Price,
                    Currency = await _announcementRepository.GetCurrencyById(request.CurrencyId),
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
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        if (!UpdateUserUploadLimits(request.IsPremium, user))
        {
            return null;
        }

        var announcement = await MapCreateAnnouncementDtoToModelAsync(request, user);
        
        if (!await _announcementRepository.CheckAllRelationsInAnnouncement(announcement))
        {
            throw new ValidationException("Invalid relationships in the announcement.");
        }
        
        // announcement.ImageUrls = await _fileService.UploadFilesAsync(request.ImageData);
        var response = await _announcementRepository.CreateAnnouncementInDbAsync(user, announcement);
        
        return _mapper.Map<AnnouncementResponseDto>(response);
    }
    
    public async Task<AnnouncementResponseDto?> DeleteInactivateAnnouncementAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
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
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

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
        var response =
            await _announcementRepository.GetAllAnnouncementsForAdminPanelFromDbAsync(parameters, announcementState);
        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }
    
    public async Task<AnnouncementResponseDto?> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var announcement = await MapUpdateAnnouncementDtoToModelAsync(request, user);
        
        if (!await _announcementRepository.CheckAllRelationsInAnnouncement(announcement))
        {
            throw new ValidationException("Invalid relationships in the announcement.");
        }
        
        var updatedAnnouncement = await _announcementRepository.UpdateAnnouncementInDbAsync(user, announcementId, announcement);

        return _mapper.Map<AnnouncementResponseDto>(updatedAnnouncement);
    }

    public async Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementActiveInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }
    
    public async Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }
        
        var response = await _announcementRepository.MakeAnnouncementInactiveInDbAsync(user, announcementId);

        return _mapper.Map<AnnouncementResponseDto>(response);
    }
    
    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        var response = await _announcementRepository.GetFilteredAnnouncementsFromDbAsync(filterParameters, pagingParameters);
        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }

    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserAsync(PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetAnnouncementsByStatesAndByUserFromDbAsync(user, pagingParameters, announcementState);

        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }
    
    public async Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserAsync(PagingParameters pagingParameters)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        var response = await _announcementRepository.GetAllAnnouncementsByUserFromDbAsync(user, pagingParameters);

        return _mapper.Map<PaginatedList<AnnouncementResponseMiniDto>>(response);
    }
}
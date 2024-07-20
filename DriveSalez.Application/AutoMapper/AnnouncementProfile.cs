using AutoMapper;
using DriveSalez.Application.DTO.AnnoucementDTO;
using DriveSalez.Application.DTO.AnnouncementDTO;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class AnnouncementProfile : Profile
{
    public AnnouncementProfile()
    {
        CreateMap<Announcement, AnnouncementResponseMiniDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrls[0]))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Vehicle.Year))
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Vehicle.Make))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Vehicle.Model))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.Vehicle.FuelType))
            .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.EngineVolume))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.MileAge))
            .ForMember(dest => dest.OnCredit, opt => opt.MapFrom(src => src.OnCredit))
            .ForMember(dest => dest.Barter, opt => opt.MapFrom(src => src.Barter))
            .ForMember(dest => dest.VinCode, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.VinCode))
            .ForMember(dest => dest.MileageType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.MileageType.ToString()));
        
       CreateMap<Announcement, AnnouncementResponseDto>()
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Vehicle.Year.Year))
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Vehicle.Make.MakeName))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Vehicle.Model.ModelName))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.Vehicle.FuelType.FuelType))
            .ForMember(dest => dest.IsBrandNew, opt => opt.MapFrom(src => src.Vehicle.IsBrandNew.Value))
            .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.BodyType.BodyType))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.Color.Color))
            .ForMember(dest => dest.HorsePower, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.HorsePower))
            .ForMember(dest => dest.GearboxType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.GearboxType.GearboxType))
            .ForMember(dest => dest.DrivetrainType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.DrivetrainType.DrivetrainType))
            .ForMember(dest => dest.Conditions, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.Conditions.Select(cond => cond.Condition).ToList()))
            .ForMember(dest => dest.MarketVersion, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.MarketVersion.MarketVersion))
            .ForMember(dest => dest.OwnerQuantity, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.OwnerQuantity.Value))
            .ForMember(dest => dest.SeatCount, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.SeatCount.Value))
            .ForMember(dest => dest.VinCode, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.VinCode))
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.Options.Select(opt => opt.Option).ToList()))
            .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.EngineVolume.Value))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.MileAge))
            .ForMember(dest => dest.MileageType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetails.MileageType.ToString()))
            .ForMember(dest => dest.AnnouncementState, opt => opt.MapFrom(src => src.AnnouncementState.ToString()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Owner.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Owner.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Owner.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Owner.LastName))
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.Owner.PhoneNumbers.Select(num => num.PhoneNumber).ToList()));
    }
} 
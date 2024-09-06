using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;
using DriveSalez.SharedKernel.Utilities;

namespace DriveSalez.Application.AutoMapper;

public class AnnouncementProfile : Profile
{
    public AnnouncementProfile()
    {
        CreateMap<Announcement, GetAnnouncementMiniDto>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImageUrls.Take(3).Select(x => x.Url)))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Year))
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Vehicle.Make))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Vehicle.Model))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.FuelType))
            .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.EngineVolume))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Mileage))
            .ForMember(dest => dest.OnCredit, opt => opt.MapFrom(src => src.OnCredit))
            .ForMember(dest => dest.Barter, opt => opt.MapFrom(src => src.Barter))
            .ForMember(dest => dest.VinCode, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.VinCode))
            .ForMember(dest => dest.DistanceUnit, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.DistanceUnit.ToString()));
        
       CreateMap<Announcement, GetAnnouncementDto>()
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Year.Year))
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Vehicle.Make.Title))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Vehicle.Model.Title))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.FuelType.Type))
            .ForMember(dest => dest.IsBrandNew, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.IsBrandNew))
            .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.BodyType.Type))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Color.Title))
            .ForMember(dest => dest.HorsePower, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.HorsePower))
            .ForMember(dest => dest.GearboxType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.GearboxType.Type))
            .ForMember(dest => dest.DrivetrainType, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.DrivetrainType.Type))
            .ForMember(dest => dest.Conditions, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Conditions.Select(cond => cond.Title).ToList()))
            .ForMember(dest => dest.MarketVersion, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.MarketVersion.Version))
            .ForMember(dest => dest.OwnerQuantity, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.OwnerQuantity))
            .ForMember(dest => dest.SeatCount, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.SeatCount))
            .ForMember(dest => dest.VinCode, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.VinCode))
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Options.Select(opt => opt.Title).ToList()))
            .ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.EngineVolume))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.Mileage))
            .ForMember(dest => dest.DistanceUnit, opt => opt.MapFrom(src => src.Vehicle.VehicleDetail.DistanceUnit.ToString()))
            .ForMember(dest => dest.AnnouncementState, opt => opt.MapFrom(src => src.AnnouncementState.ToString()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Owner.Id))
            // .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Owner.UserName))
            // .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Owner.Email))
            // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
            // .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Owner.LastName))
            // .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.Owner.PhoneNumbers.Select(num => num.Number).ToList()))
            // .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Owner.PhoneNumber))
            // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
            // .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Owner.LastName))
            // .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Owner.Address))
            // .ForMember(dest => dest.WorkHours, opt => opt.MapFrom(src => src.Owner.WorkHours))
            // .ForMember(dest => dest.ProfilePhotoUrl, opt => opt.MapFrom(src => src.Owner.ProfilePhotoUrl))
            // .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Owner.Description))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name));
       
       CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
           .ConvertUsing(typeof(PaginatedListConverter<,>));

       CreateMap<Tuple<IEnumerable<Announcement>, PaginatedList<Announcement>>, 
           Tuple<IEnumerable<GetAnnouncementMiniDto>, PaginatedList<GetAnnouncementMiniDto>>>();
    }
    
    private class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            var items = context.Mapper.Map<List<TDestination>>(source);
            return new PaginatedList<TDestination>(items, source.PageIndex, source.TotalPages, source.TotalCount);
        }
    }
} 
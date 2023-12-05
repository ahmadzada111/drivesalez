using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;

namespace DriveSalez.Infrastructure.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Announcement, AnnouncementResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Owner.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Owner.LastName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Owner.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Owner.Email))
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.Owner.PhoneNumbers));
        
        CreateMap<AnnouncementResponseDto, Announcement>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
            .ForMember(dest => dest.Barter, opt => opt.MapFrom(src => src.Barter))
            .ForMember(dest => dest.OnCredit, opt => opt.MapFrom(src => src.OnCredit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.AnnouncementState, opt => opt.MapFrom(src => src.AnnouncementState))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImageUrls))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.IsPremium, opt =>opt.MapFrom(src => src.IsPremium)) 
            .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => src.ViewCount)); 
    }
} 
using AutoMapper;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;

namespace DriveSalez.Infrastructure.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Announcement, AnnouncementResponseDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Owner.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Owner.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Owner.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Owner.PhoneNumber));
    }
} 
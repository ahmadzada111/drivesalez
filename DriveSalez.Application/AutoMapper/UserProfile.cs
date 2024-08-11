using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Application.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, GetUserDto>();
    }
}
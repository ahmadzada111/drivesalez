using AutoMapper;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;

namespace DriveSalez.Application.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, GetUserDto>();
    }
}
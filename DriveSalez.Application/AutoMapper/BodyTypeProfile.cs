using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.AutoMapper;

public class BodyTypeProfile : Profile
{
    public BodyTypeProfile()
    {
        CreateMap<BodyType, BodyTypeDto>();

        CreateMap<BodyTypeDto, BodyType>();
    }
}
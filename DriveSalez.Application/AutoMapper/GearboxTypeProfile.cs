using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.AutoMapper;

public class GearboxTypeProfile : Profile
{
    public GearboxTypeProfile()
    {
        CreateMap<GearboxType, GearboxTypeDto>();

        CreateMap<GearboxTypeDto, GearboxType>();
    }
}
using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.GearboxTypeDTO;

namespace DriveSalez.Application.AutoMapper;

public class GearboxTypeProfile : Profile
{
    public GearboxTypeProfile()
    {
        CreateMap<GearboxType, GearboxTypeDto>();

        CreateMap<GearboxTypeDto, GearboxType>();
    }
}
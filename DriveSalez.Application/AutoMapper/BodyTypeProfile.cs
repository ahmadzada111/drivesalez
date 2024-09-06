using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.BodyTypeDTO;

namespace DriveSalez.Application.AutoMapper;

public class BodyTypeProfile : Profile
{
    public BodyTypeProfile()
    {
        CreateMap<BodyType, BodyTypeDto>();

        CreateMap<BodyTypeDto, BodyType>();
    }
}
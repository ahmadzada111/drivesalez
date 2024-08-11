using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.AutoMapper;

public class DrivetrainTypeProfile : Profile
{
    public DrivetrainTypeProfile()
    {
        CreateMap<DrivetrainType, DrivetrainTypeDto>();

        CreateMap<DrivetrainTypeDto, DrivetrainType>();
    }
}
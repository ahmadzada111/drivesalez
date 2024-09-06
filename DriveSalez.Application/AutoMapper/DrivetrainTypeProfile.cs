using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.DrivetrainTypeDTO;

namespace DriveSalez.Application.AutoMapper;

public class DrivetrainTypeProfile : Profile
{
    public DrivetrainTypeProfile()
    {
        CreateMap<DrivetrainType, DrivetrainTypeDto>();

        CreateMap<DrivetrainTypeDto, DrivetrainType>();
    }
}
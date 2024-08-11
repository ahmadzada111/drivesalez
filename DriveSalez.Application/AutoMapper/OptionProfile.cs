using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Application.AutoMapper;

public class OptionProfile : Profile
{
    public OptionProfile()
    {
        CreateMap<Option, OptionDto>();

        CreateMap<OptionDto, Option>();
    }
}
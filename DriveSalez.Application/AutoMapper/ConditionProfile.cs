using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Application.AutoMapper;

public class ConditionProfile : Profile
{
    public ConditionProfile()
    {
        CreateMap<Condition, ConditionDto>();

        CreateMap<ConditionDto, Condition>();
    }
}
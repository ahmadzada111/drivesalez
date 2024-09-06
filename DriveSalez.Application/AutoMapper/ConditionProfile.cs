using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.ConditionDTO;

namespace DriveSalez.Application.AutoMapper;

public class ConditionProfile : Profile
{
    public ConditionProfile()
    {
        CreateMap<Condition, ConditionDto>();

        CreateMap<ConditionDto, Condition>();
    }
}
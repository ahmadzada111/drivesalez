using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.OptionDTO;

namespace DriveSalez.Application.AutoMapper;

public class OptionProfile : Profile
{
    public OptionProfile()
    {
        CreateMap<Option, OptionDto>();

        CreateMap<OptionDto, Option>();
    }
}
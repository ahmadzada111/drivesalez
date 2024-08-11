using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class ManufactureYearProfile : Profile
{
    public ManufactureYearProfile()
    {
        CreateMap<ManufactureYear, ManufactureYearDto>();

        CreateMap<ManufactureYearDto, ManufactureYear>();
    }
}
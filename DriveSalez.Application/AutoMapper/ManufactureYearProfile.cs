using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.ManufactureYearDTO;

namespace DriveSalez.Application.AutoMapper;

public class ManufactureYearProfile : Profile
{
    public ManufactureYearProfile()
    {
        CreateMap<ManufactureYear, ManufactureYearDto>();

        CreateMap<ManufactureYearDto, ManufactureYear>();
    }
}
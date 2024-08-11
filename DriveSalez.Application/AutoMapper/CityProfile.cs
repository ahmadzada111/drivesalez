using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();

        CreateMap<CityDto, City>();
    }
}
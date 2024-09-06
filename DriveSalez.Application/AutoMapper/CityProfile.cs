using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.CityDTO;

namespace DriveSalez.Application.AutoMapper;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, GetCityDto>();

        CreateMap<GetCityDto, City>();
    }
}
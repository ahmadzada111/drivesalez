using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();

        CreateMap<CountryDto, Country>();
    }
}
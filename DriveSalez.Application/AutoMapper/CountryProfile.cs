using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.CountryDTO;

namespace DriveSalez.Application.AutoMapper;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, GetCountryDto>();

        CreateMap<GetCountryDto, Country>();
    }
}
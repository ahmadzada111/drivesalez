using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.MarketVersionDTO;

namespace DriveSalez.Application.AutoMapper;

public class MarketVersionProfile : Profile
{
    public MarketVersionProfile()
    {
        CreateMap<MarketVersion, MarketVersionDto>();

        CreateMap<MarketVersionDto, MarketVersion>();
    }
}
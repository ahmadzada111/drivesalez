using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.AutoMapper;

public class MarketVersionProfile : Profile
{
    public MarketVersionProfile()
    {
        CreateMap<MarketVersion, MarketVersionDto>();

        CreateMap<MarketVersionDto, MarketVersion>();
    }
}
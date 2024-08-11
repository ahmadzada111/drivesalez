using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.AutoMapper;

public class FuelTypeProfile : Profile
{
    public FuelTypeProfile()
    {
        CreateMap<FuelType, FuelTypeDto>();

        CreateMap<FuelTypeDto, FuelType>();
    }
}
using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.FuelTypeDTO;

namespace DriveSalez.Application.AutoMapper;

public class FuelTypeProfile : Profile
{
    public FuelTypeProfile()
    {
        CreateMap<FuelType, FuelTypeDto>();

        CreateMap<FuelTypeDto, FuelType>();
    }
}
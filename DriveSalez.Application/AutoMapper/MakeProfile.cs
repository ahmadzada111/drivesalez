using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class MakeProfile : Profile
{
    public MakeProfile()
    {
        CreateMap<Make, MakeDto>();

        CreateMap<MakeDto, Make>();
    }
}
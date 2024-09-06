using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.MakeDTO;

namespace DriveSalez.Application.AutoMapper;

public class MakeProfile : Profile
{
    public MakeProfile()
    {
        CreateMap<Make, GetMakeDto>();

        CreateMap<GetMakeDto, Make>();
    }
}
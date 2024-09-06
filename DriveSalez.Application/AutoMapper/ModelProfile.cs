using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.ModelDTO;

namespace DriveSalez.Application.AutoMapper;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<Model, ModelDto>();

        CreateMap<ModelDto, Model>();
    }
}
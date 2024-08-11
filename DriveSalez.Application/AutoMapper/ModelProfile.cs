using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<Model, ModelDto>();

        CreateMap<ModelDto, Model>();
    }
}
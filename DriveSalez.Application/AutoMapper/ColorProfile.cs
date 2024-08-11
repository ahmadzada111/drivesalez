using System.Drawing;
using AutoMapper;
using DriveSalez.Application.DTO;

namespace DriveSalez.Application.AutoMapper;

public class ColorProfile : Profile
{
    public ColorProfile()
    {
        CreateMap<Color, ColorDto>();

        CreateMap<ColorDto, Color>();
    }
}
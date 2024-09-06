using System.Drawing;
using AutoMapper;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.ColorDTO;

namespace DriveSalez.Application.AutoMapper;

public class ColorProfile : Profile
{
    public ColorProfile()
    {
        CreateMap<Color, ColorDto>();

        CreateMap<ColorDto, Color>();
    }
}
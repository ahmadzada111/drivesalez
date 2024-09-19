using AutoMapper;
using DriveSalez.Domain.Entities;
using DriveSalez.SharedKernel.DTO;

namespace DriveSalez.Application.AutoMapper;

public class WorkHourProfile : Profile
{
    public WorkHourProfile()
    {
        CreateMap<WorkHourDto, WorkHour>();
        CreateMap<WorkHour, WorkHourDto>();
    }
}
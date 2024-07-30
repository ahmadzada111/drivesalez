using AutoMapper;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class PhoneNumberProfile : Profile
{
    public PhoneNumberProfile()
    {
        CreateMap<PhoneNumber, string>()
            .ConvertUsing(phoneNumber => phoneNumber.Number);;

        CreateMap<string, PhoneNumber>()
            .ConvertUsing(phoneNumber => new PhoneNumber {Number = phoneNumber});
    }
}
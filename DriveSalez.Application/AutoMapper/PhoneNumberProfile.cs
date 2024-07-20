using AutoMapper;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.AutoMapper;

public class PhoneNumberProfile : Profile
{
    public PhoneNumberProfile()
    {
        CreateMap<AccountPhoneNumber, string>()
            .ConvertUsing(phoneNumber => phoneNumber.PhoneNumber);;

        CreateMap<string, AccountPhoneNumber>()
            .ConvertUsing(phoneNumber => new AccountPhoneNumber {PhoneNumber = phoneNumber});
    }
}
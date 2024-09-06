using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Application;

public static class DependencyInjection
{
    public static IServiceCollection InjectApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAnnouncementService, AnnouncementService>();
        services.AddScoped<IBodyTypeService, BodyTypeService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IColorService, ColorService>();
        services.AddScoped<IConditionService, ConditionService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDrivetrainTypeService, DrivetrainTypeService>();
        services.AddScoped<IFuelTypeService, FuelTypeService>();
        services.AddScoped<IGearboxTypeService, GearboxTypeService>();
        services.AddScoped<IMakeService, MakeService>();
        services.AddScoped<IManufactureYearService, ManufactureYearService>();
        services.AddScoped<IMarketVersionService, MarketVersionService>();
        services.AddScoped<IModelService, ModelService>();
        services.AddScoped<IOptionService, OptionService>();

        return services;
    }
}
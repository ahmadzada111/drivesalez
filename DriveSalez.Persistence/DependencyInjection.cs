using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.Persistence.Repositories;
using DriveSalez.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection InjectPersistenceLayer(this IServiceCollection services)
    {
        services.AddScoped<ICarQueryService, CarQueryService>();
        services.AddScoped<IPayPalService, PayPalService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IEmailService, EmailService>();
        
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IBodyTypeRepository, BodyTypeRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<IConditionRepository, ConditionRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IDrivetrainTypeRepository, DrivetrainTypeRepository>();
        services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
        services.AddScoped<IGearboxTypeRepository, GearboxTypeRepository>();
        services.AddScoped<IMakeRepository, MakeRepository>();
        services.AddScoped<IManufactureYearRepository, ManufactureYearRepository>();
        services.AddScoped<IMarketVersionRepository, MarketVersionRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<IOptionRepository, OptionRepository>();
        services.AddScoped<IImageUrlRepository, ImageUrlRepository>();
        services.AddScoped<IOneTimePurchaseRepository, OneTimePurchaseRepository>();
        services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUserLimitRepository, UserLimitRepository>();
        services.AddScoped<IUserPurchaseRepository, UserPurchaseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleDetailRepository, VehicleDetailRepository>();
        
        return services;
    }
}
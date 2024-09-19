using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Persistence.Repositories;
using DriveSalez.Persistence.Services;
using DriveSalez.SharedKernel.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

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
        services.AddScoped<IUserRoleLimitRepository, UserRoleLimitRepository>();
        services.AddScoped<IWorkHourRepository, WorkHourRepository>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        return services;
    }
    
    public static IServiceCollection ConfigureFluentEmail(this IServiceCollection services, IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>() 
                            ?? throw new InvalidOperationException($"{nameof(EmailSettings)} cannot be null");

        services.AddFluentEmail(emailSettings.CompanyEmail)
            .AddSmtpSender(emailSettings.SmtpServer, emailSettings.Port, emailSettings.CompanyEmail, emailSettings.EmailKey);

        return services;
    }
    
    public static IServiceCollection ConfigureQuartz(this IServiceCollection services)
    {
        services.AddQuartz();
        
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        // services.ConfigureOptions<CheckAnnouncementExpirationJobSetup>();
        // services.ConfigureOptions<DeleteInactiveAccountsJobSetup>();
        // services.ConfigureOptions<LookForExpiredPremiumAnnouncementJobSetup>();
        // services.ConfigureOptions<NotifyUserAboutSubscriptionCancellationJobSetup>();
        // services.ConfigureOptions<NotifyUsersWithExpiringSubscriptionsJobSetup>();
        // services.ConfigureOptions<RenewLimitsForDefaultUserJobSetup>();
        
        return services;
    }
}
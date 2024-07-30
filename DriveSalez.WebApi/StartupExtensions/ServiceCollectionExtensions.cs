using DriveSalez.Application.ServiceContracts;
using DriveSalez.Application.Services;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using DriveSalez.Application.AutoMapper;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.Settings;
using Quartz;

namespace DriveSalez.WebApi.StartupExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<JwtSettings>();
        services.AddSingleton<EmailSettings>();
        services.AddSingleton<RefreshTokenSettings>();
        services.AddSingleton<PayPalSettings>();
        services.AddSingleton<BlobStorageSettings>();
        services.AddScoped<IPayPalService, PayPalService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IModeratorService, ModeratorService>();
        services.AddScoped<IModeratorRepository, ModeratorRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAnnouncementService, AnnouncementService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IDetailsService, DetailsService>();
        services.AddScoped<IDetailsRepository, DetailsRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }
    
    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AnnouncementProfile>();
            cfg.AddProfile<PhoneNumberProfile>();
            cfg.AddProfile<UserProfile>();
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DriveSaleZ API",
                Version = "v1"
            });

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer <token>\""
            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
    
    public static IServiceCollection ConfigureAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabaseContext(configuration);
        services.ConfigureIdentity();
        services.ConfigureJwtAuthentication(configuration);
        services.ConfigureAuthorizationPolicies();

        return services;
    }

    private static IServiceCollection ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        return services;
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
        .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

        return services;
    }

    private static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });

        return services;
    }

    private static IServiceCollection ConfigureAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }
    
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DriveSalezCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

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
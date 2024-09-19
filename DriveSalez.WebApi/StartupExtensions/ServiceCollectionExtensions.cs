using DriveSalez.Persistence.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Asp.Versioning;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Settings;
using Quartz;
using FluentValidation;
using DriveSalez.Application;
using DriveSalez.Persistence;
using AssemblyReference = DriveSalez.Presentation.AssemblyReference;

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

        services.InjectApplicationLayer();
        services.InjectPersistenceLayer();
        
        return services;
    }
    
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(e =>
        {
            e.AssumeDefaultVersionWhenUnspecified = true;
            e.DefaultApiVersion = new ApiVersion(1);
            e.ReportApiVersions = true;
            e.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(e =>
        {
            e.GroupNameFormat = "'v'V";
            e.SubstituteApiVersionInUrl = true;
        });

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
            
            var xmlFile = $"{AssemblyReference.Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            setup.IncludeXmlComments(xmlPath);
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

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
        .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

        return services;
    }

    private static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() 
                          ?? throw new InvalidOperationException($"{nameof(JwtSettings)} cannot be null");

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
}
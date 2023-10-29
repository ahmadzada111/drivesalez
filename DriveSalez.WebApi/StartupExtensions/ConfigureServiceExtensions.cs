using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Core.Services;
using DriveSalez.Infrastructure.DbContext;
using DriveSalez.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using DriveSalez.Infrastructure.AutoMapper;
using DriveSalez.Infrastructure.Quartz.Jobs;
using Quartz;

namespace DriveSalez.WebApi.StartupExtensions;

public static class ConfigureServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IBlobContainerClientProvider, BlobContainerClientProvider>();
        services.AddSingleton<IFileService, FileService>();
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

        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "DriveSaleZ",
                    Version = "v2"
                }
            );

            //setup.IncludeXmlComments(@"obj\Debug\net6.0\ToDo WEB API.xml");

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer\""
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
                    new string[] { }
                }
            });
        });
        return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => { options.UseSqlServer(configuration.GetConnectionString("MacConnection")); }
        );

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
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }

    public static IServiceCollection AddCorsToServices(this IServiceCollection services)
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
    
    public static IServiceCollection AddQuartzToServices(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.SchedulerId = "ds_scheduler";
            q.SchedulerName = "DriveSalezQuartzScheduler";
            
            var checkAnnouncementStateKey = new JobKey("CheckAnnouncementExpiration");
            q.AddJob<CheckAnnouncementExpirationJob>(opts => opts.WithIdentity(checkAnnouncementStateKey)
                .StoreDurably());

            q.AddTrigger(opts => opts
                .ForJob(checkAnnouncementStateKey)
                .WithIdentity("CheckAnnouncementExpiration-trigger")
                .WithCronSchedule("0 0 1 1 * ?")
                .StartNow());
            
            var deleteInactiveAccountsKey = new JobKey("DeleteInactiveAccounts");
            q.AddJob<DeleteInactiveAccountsJob>(opts => opts.WithIdentity(deleteInactiveAccountsKey)
                .StoreDurably());

            q.AddTrigger(opts => opts
                .ForJob(deleteInactiveAccountsKey)
                .WithIdentity("DeleteInactiveAccounts-trigger")
                .WithCronSchedule("0 0 1 1 * ?")
                .StartNow());
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
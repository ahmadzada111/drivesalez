using System.Text.Json.Serialization;
using DriveSalez.Presentation;
using DriveSalez.SharedKernel.Settings;
using DriveSalez.WebApi.Middleware;

namespace DriveSalez.WebApi.StartupExtensions;

public static class ProgramConfigurationExtensions
{
    public static void ConfigureSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables(); 

        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }
        
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.AddSingleton<JwtSettings>();
        
        builder.Services.Configure<RefreshTokenSettings>(builder.Configuration.GetSection("RefreshTokenSettings"));
        builder.Services.AddSingleton<RefreshTokenSettings>();

        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddSingleton<EmailSettings>();
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddApplicationPart(typeof(AssemblyReference).Assembly);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureSwagger();
        builder.Services.RegisterApplicationServices();
        builder.Services.ConfigureAuthenticationAndAuthorization(builder.Configuration);
        builder.Services.ConfigureQuartz();
        builder.Services.ConfigureCors();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddLogging();
        builder.Services.ConfigureAutoMapper();
        builder.Services.AddMemoryCache();
    }

    public static void ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.EnablePersistAuthorization());
        }
        else
        {
            app.UseExceptionHandler();
        }

        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseCors("DriveSalezCorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void ConfigureEndpoints(this WebApplication app)
    {
        app.MapControllers();
    }
}
using DriveSalez.WebApi.StartupExtensions;

namespace DriveSalez.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            
        builder.ConfigureSettings();
        builder.RegisterServices();
            
        var app = builder.Build();
            
        app.ConfigureMiddleware();
        app.ConfigureEndpoints();
            
        await app.SeedData();
        await app.RunAsync();
    }
}
using DriveSalez.WebApi.StartupExtensions;
using System.Text.Json.Serialization;
using DriveSalez.Infrastructure.Quartz.Jobs;
using DriveSalez.WebApi.Middleware;
using Quartz;
using Quartz.AspNetCore;

namespace DriveSalez.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var configuration = builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Secrets.json")
                .Build();

            builder.Services.AddServices();
            builder.Services.AddAuthenticationAndAuthorization(configuration);
            builder.Services.AddQuartzToServices();
            builder.Services.AddSwagger();
            builder.Services.AddCorsToServices();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddLogging();
            builder.Services.AddMapper();
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x => x.EnablePersistAuthorization());
            }
            else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }
            
            app.UseHsts();
            app.UseHttpsRedirection();
            
            app.UseCors("DriveSalezCorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
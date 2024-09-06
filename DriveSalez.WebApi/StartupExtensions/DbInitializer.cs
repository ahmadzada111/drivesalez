using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.WebApi.StartupExtensions;

public static class DbInitializer
{
    public static async Task<WebApplication> SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var carQueryService = scope.ServiceProvider.GetRequiredService<ICarQueryService>();
        
        await context.Database.EnsureCreatedAsync();

        var roles = await context.Roles.ToListAsync();

        if (roles.IsNullOrEmpty())
        {

            var makes = await carQueryService.GetAllMakesAsync();
            var bodyTypes = await carQueryService.GetBodyTypesAsync();
            var fuelTypes = await carQueryService.GetFuelTypesAsync();
            var drivetrainTypes = await carQueryService.GetDrivetrainTypesAsync();

            foreach (var make in makes)
            {
                var models = await carQueryService.GetModelsByMakeAsync(make.Title);
                make.Models.ToList().AddRange(models);
            }

            await context.Makes.AddRangeAsync(makes);
            await context.BodyTypes.AddRangeAsync(bodyTypes);
            await context.FuelTypes.AddRangeAsync(fuelTypes);
            await context.DrivetrainTypes.AddRangeAsync(drivetrainTypes);

            await context.Roles.AddRangeAsync(new ApplicationRole
                {
                    Id = Guid.Parse("8c36e548-f0cc-4ef5-b3b0-33b67b92d8ce"),
                    Name = UserType.DefaultUser.ToString(),
                    NormalizedName = UserType.DefaultUser.ToString().ToUpperInvariant()
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("20da09a9-5f13-4df0-a44d-8f5b9a137332"),
                    Name = UserType.BusinessUser.ToString(),
                    NormalizedName = UserType.BusinessUser.ToString().ToUpperInvariant()
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("6faa4cd6-6cb6-4ec8-a3c4-8e7e4a03e373"),
                    Name = UserType.Admin.ToString(),
                    NormalizedName = UserType.Admin.ToString().ToUpperInvariant(),
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("7e1727bb-c257-45b4-a723-4f759bcb0866"),
                    Name = UserType.Moderator.ToString(),
                    NormalizedName = UserType.Moderator.ToString().ToUpperInvariant(),
                });
            
            // await context.Users.AddAsync(new ApplicationUser()
            // {
            //     Id = Guid.Parse("e0a73daa-1c33-4567-99ab-fd87f12b5176"),
            //     Email = "admin",
            //     UserName = "admin",
            //     FirstName = "admin",
            //     LastName = "admin",
            //     PhoneNumber = "911",
            //     EmailConfirmed = true
            // });
            //
            // await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
            // {
            //     UserId = Guid.Parse("e0a73daa-1c33-4567-99ab-fd87f12b5176"),
            //     RoleId = Guid.Parse("6faa4cd6-6cb6-4ec8-a3c4-8e7e4a03e373")
            // });

            // context.MarketVersions.Add(new MarketVersion() { Version = "USA" });
            // context.MarketVersions.Add(new MarketVersion() { Version = "Azerbaijan" });
            // context.MarketVersions.Add(new MarketVersion() { Version = "Korea" });
            // context.MarketVersions.Add(new MarketVersion() { Version = "Europe" });

            // context.Colors.Add(new Color() { Title = "White" });
            // context.Colors.Add(new Color() { Title = "Black" });
            // context.Colors.Add(new Color() { Title = "Red" });
            // context.Colors.Add(new Color() { Title = "Yellow" });
            // context.Colors.Add(new Color() { Title = "Green" });
            // context.Colors.Add(new Color() { Title = "Blue" });

            // var azerbaijan = context.Countries.Add(new Country() { Name = "Azerbaijan" }).Entity;
            // var poland = context.Countries.Add(new Country() { Name = "Poland" }).Entity;
            
            // context.Cities.Add(new City() { Name = "Baku", Country = azerbaijan });
            // context.Cities.Add(new City() { Name = "Quba", Country = azerbaijan });
            // context.Cities.Add(new City() { Name = "Qah", Country = azerbaijan });

            // context.Cities.Add(new City() { Name = "Warsaw", Country = poland });
            // context.Cities.Add(new City() { Name = "Krakow", Country = poland });
            // context.Cities.Add(new City() { Name = "Gdansk", Country = poland });
            
            // context.Options.Add(new Option() { Title = "Alloy Wheels" });
            // context.Options.Add(new Option() { Title = "ABS" });
            // context.Options.Add(new Option() { Title = "Sunroof" });
            // context.Options.Add(new Option() { Title = "Rain sensor" });
            // context.Options.Add(new Option() { Title = "Central locking" });
            // context.Options.Add(new Option() { Title = "Parking sensors" });
            // context.Options.Add(new Option() { Title = "Air conditioning" });
            // context.Options.Add(new Option() { Title = "Heated seats" });
            // context.Options.Add(new Option() { Title = "Leather interior" });
            // context.Options.Add(new Option() { Title = "Xenon headlights" });
            // context.Options.Add(new Option() { Title = "Rear view camera" });
            // context.Options.Add(new Option() { Title = "Side curtains" });
            // context.Options.Add(new Option() { Title = "Seat ventilation" });

            // context.Conditions.Add(new Condition() 
            // { 
            //         Title= "Damaged", 
            //         Description= "One or more parts have been replaced or repaired." 
            // });
            // context.Conditions.Add(new Condition()
            // {
            //     Title= "Painted", 
            //     Description= "One or more parts have been repainted or cosmetic work has been done."
            // });
            // context.Conditions.Add(new Condition()
            // {
            //     Title= "Crashed or for parts", 
            //     Description= "Needs repair or is completely unfit for use."
            // });
            //
            // context.ManufactureYears.Add(new ManufactureYear() { Year = 2023 });
            // context.ManufactureYears.Add(new ManufactureYear() { Year = 2022 });
            // context.ManufactureYears.Add(new ManufactureYear() { Year = 2021 });
            // context.ManufactureYears.Add(new ManufactureYear() { Year = 2020 });
            // context.ManufactureYears.Add(new ManufactureYear() { Year = 2019 });

            // context.PricingOptions.Add(new PricingOption() { Title = nameof(BusinessAccount), Price = 10 });
            // context.PricingOptions.Add(new PricingOption() { Title = "Premium Announcement", Price = 10 });
            // context.PricingOptions.Add(new PricingOption() { Title = "Regular Announcement", Price = 10 });
            //
            // context.AccountLimits.Add(new AccountLimit()
            // {
            //     PremiumAnnouncementsLimit = 0,
            //     RegularAnnouncementsLimit = 5, 
            //     UserType = UserType.DefaultAccount
            // });
            // context.AccountLimits.Add(new AccountLimit()
            // {
            //     PremiumAnnouncementsLimit = 20, 
            //     RegularAnnouncementsLimit = 50, 
            //     UserType = UserType.BusinessAccount
            // });
            //     
            await context.SaveChangesAsync();
        }
        
        return app;
    }
}
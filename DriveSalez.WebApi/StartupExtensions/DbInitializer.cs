using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Persistence.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.WebApi.StartupExtensions;

public static class DbInitializer
{
    public static async Task<WebApplication> SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Database.EnsureCreatedAsync();

        var roles = await context.Roles.ToListAsync();

        if (roles.IsNullOrEmpty())
        {
            await context.Roles.AddRangeAsync(new ApplicationRole
                {
                    Id = Guid.Parse("8c36e548-f0cc-4ef5-b3b0-33b67b92d8ce"),
                    Name = UserType.DefaultAccount.ToString(),
                    NormalizedName = UserType.DefaultAccount.ToString().ToUpperInvariant()
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("20da09a9-5f13-4df0-a44d-8f5b9a137332"),
                    Name = UserType.BusinessAccount.ToString(),
                    NormalizedName = UserType.BusinessAccount.ToString().ToUpperInvariant()
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

            await context.Users.AddAsync(new DefaultAccount()
            {
                Id = Guid.Parse("e0a73daa-1c33-4567-99ab-fd87f12b5176"),
                Email = "admin",
                UserName = "admin",
                FirstName = "admin",
                LastName = "admin"
            });

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
            {
                UserId = Guid.Parse("e0a73daa-1c33-4567-99ab-fd87f12b5176"),
                RoleId = Guid.Parse("6faa4cd6-6cb6-4ec8-a3c4-8e7e4a03e373")
            });

            context.VehicleMarketVersions.Add(new MarketVersion() { Version = "USA" });
            context.VehicleMarketVersions.Add(new MarketVersion() { Version = "Azerbaijan" });
            context.VehicleMarketVersions.Add(new MarketVersion() { Version = "Korea" });
            context.VehicleMarketVersions.Add(new MarketVersion() { Version = "Europe" });

            context.VehicleColors.Add(new Color() { Title = "White" });
            context.VehicleColors.Add(new Color() { Title = "Black" });
            context.VehicleColors.Add(new Color() { Title = "Red" });
            context.VehicleColors.Add(new Color() { Title = "Yellow" });
            context.VehicleColors.Add(new Color() { Title = "Green" });
            context.VehicleColors.Add(new Color() { Title = "Blue" });

            context.VehicleBodyTypes.Add(new BodyType() { Type = "Sedan" });
            context.VehicleBodyTypes.Add(new BodyType() { Type = "Coupe" });
            context.VehicleBodyTypes.Add(new BodyType() { Type = "Cabriolet" });
            context.VehicleBodyTypes.Add(new BodyType() { Type = "Station Wagon" });
            context.VehicleBodyTypes.Add(new BodyType() { Type = "SUV/Off-Road" });
            context.VehicleBodyTypes.Add(new BodyType() { Type = "Pick-Up" });

            context.VehicleGearboxTypes.Add(new GearboxType() { Type = "Manual" });
            context.VehicleGearboxTypes.Add(new GearboxType() { Type = "Auto" });
            context.VehicleGearboxTypes.Add(new GearboxType() { Type = "Robotic" });

            context.VehicleDriveTrainTypes.Add(new DrivetrainType() { Type = "FWD" });
            context.VehicleDriveTrainTypes.Add(new DrivetrainType() { Type = "RWD" });
            context.VehicleDriveTrainTypes.Add(new DrivetrainType() { Type = "AWD" });

            var bmw = context.Makes.Add(new Make() { Title = "BMW" }).Entity;
            var mercedes = context.Makes.Add(new Make() { Title = "Mercedes-Benz" }).Entity;
            var audi = context.Makes.Add(new Make() { Title = "Audi" }).Entity;
            var ford = context.Makes.Add(new Make() { Title = "Ford" }).Entity;

            context.Models.Add(new Model() { Title = "M5", Make = bmw });
            context.Models.Add(new Model() { Title = "M8", Make = bmw });
            context.Models.Add(new Model() { Title = "M3", Make = bmw });
            context.Models.Add(new Model() { Title = "M2", Make = bmw });

            context.Models.Add(new Model() { Title = "C200", Make = mercedes });
            context.Models.Add(new Model() { Title = "C220", Make = mercedes });
            context.Models.Add(new Model() { Title = "C230", Make = mercedes });
            context.Models.Add(new Model() { Title = "C240", Make = mercedes });

            context.Models.Add(new Model() { Title = "RS6", Make = audi });
            context.Models.Add(new Model() { Title = "RS7", Make = audi });
            context.Models.Add(new Model() { Title = "RS4", Make = audi });
            context.Models.Add(new Model() { Title = "RS3", Make = audi });

            context.Models.Add(new Model() { Title = "GT", Make = ford });
            context.Models.Add(new Model() { Title = "Mondeo", Make = ford });
            context.Models.Add(new Model() { Title = "F150", Make = ford });
            context.Models.Add(new Model() { Title = "F500", Make = ford });

            context.VehicleFuelTypes.Add(new FuelType() { Type = "Gasoline" });

            var azerbaijan = context.Countries.Add(new Country() { Name = "Azerbaijan" }).Entity;
            var poland = context.Countries.Add(new Country() { Name = "Poland" }).Entity;
            
            context.Cities.Add(new City() { Name = "Baku", Country = azerbaijan });
            context.Cities.Add(new City() { Name = "Quba", Country = azerbaijan });
            context.Cities.Add(new City() { Name = "Qah", Country = azerbaijan });

            context.Cities.Add(new City() { Name = "Warsaw", Country = poland });
            context.Cities.Add(new City() { Name = "Krakow", Country = poland });
            context.Cities.Add(new City() { Name = "Gdansk", Country = poland });
            
            context.VehicleDetailsOptions.Add(new Option() { Title = "Alloy Wheels" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "ABS" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Sunroof" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Rain sensor" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Central locking" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Parking sensors" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Air conditioning" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Heated seats" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Leather interior" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Xenon headlights" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Rearview camera" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Side curtains" });
            context.VehicleDetailsOptions.Add(new Option() { Title = "Seat ventilation" });

            context.VehicleDetailsConditions.Add(new Condition() 
            { 
                    Title= "Damaged", 
                    Description= "One or more parts have been replaced or repaired." 
            });
            context.VehicleDetailsConditions.Add(new Condition()
            {
                Title= "Painted", 
                Description= "One or more parts have been repainted or cosmetic work has been done."
            });
            context.VehicleDetailsConditions.Add(new Condition()
            {
                Title= "Crashed or for parts", 
                Description= "Needs repair or is completely unfit for use."
            });

            context.ManufactureYears.Add(new ManufactureYear() { Year = 2023 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2022 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2021 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2020 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2019 });

            var subPrice = context.SubscriptionPrices.Add(new PriceDetail() { Price = 5}).Entity;

            context.Subscriptions.Add(new Subscription() { Title = "Premium Account", Price = subPrice });
            context.Subscriptions.Add(new Subscription() { Title = "Business Account", Price = subPrice });
            context.AnnouncementPricing.Add(new AnnouncementTypePricing() { Title = "Premium Announcement", Price = subPrice });
            context.AnnouncementPricing.Add(new AnnouncementTypePricing() { Title = "Regular Announcement", Price = subPrice });
            
            context.AccountLimits.Add(new AccountLimit()
            {
                PremiumAnnouncementsLimit = 0,
                RegularAnnouncementsLimit = 5, 
                UserType = UserType.DefaultAccount
            });
            context.AccountLimits.Add(new AccountLimit()
            {
                PremiumAnnouncementsLimit = 20, 
                RegularAnnouncementsLimit = 50, 
                UserType = UserType.BusinessAccount
            });
                
            await context.SaveChangesAsync();
        }
        
        return app;
    }
}
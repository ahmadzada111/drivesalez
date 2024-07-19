using DriveSalez.Core.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
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
                    Id = Guid.Parse("4d329e8c-8953-4116-bc85-17c86e7c1602"),
                    Name = UserType.PremiumAccount.ToString(),
                    NormalizedName = UserType.PremiumAccount.ToString().ToUpperInvariant()
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

            context.VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "USA" });
            context.VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "Azerbaijan" });
            context.VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "Korea" });
            context.VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "Europe" });

            context.VehicleColors.Add(new VehicleColor() { Color = "White" });
            context.VehicleColors.Add(new VehicleColor() { Color = "Black" });
            context.VehicleColors.Add(new VehicleColor() { Color = "Red" });
            context.VehicleColors.Add(new VehicleColor() { Color = "Yellow" });
            context.VehicleColors.Add(new VehicleColor() { Color = "Green" });
            context.VehicleColors.Add(new VehicleColor() { Color = "Blue" });

            context.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Sedan" });
            context.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Coupe" });
            context.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Cabriolet" });
            context.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Station Wagon" });
            context.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "SUV/Off-Road" });
            context.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Pick-Up" });

            context.VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Manual" });
            context.VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Auto" });
            context.VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Robotic" });

            context.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "FWD" });
            context.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "RWD" });
            context.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "AWD" });

            var bmw = context.Makes.Add(new Make() { MakeName = "BMW" }).Entity;
            var mercedes = context.Makes.Add(new Make() { MakeName = "Mercedes-Benz" }).Entity;
            var audi = context.Makes.Add(new Make() { MakeName = "Audi" }).Entity;
            var ford = context.Makes.Add(new Make() { MakeName = "Ford" }).Entity;

            context.Models.Add(new Model() { ModelName = "M5", Make = bmw });
            context.Models.Add(new Model() { ModelName = "M8", Make = bmw });
            context.Models.Add(new Model() { ModelName = "M3", Make = bmw });
            context.Models.Add(new Model() { ModelName = "M2", Make = bmw });

            context.Models.Add(new Model() { ModelName = "C200", Make = mercedes });
            context.Models.Add(new Model() { ModelName = "C220", Make = mercedes });
            context.Models.Add(new Model() { ModelName = "C230", Make = mercedes });
            context.Models.Add(new Model() { ModelName = "C240", Make = mercedes });

            context.Models.Add(new Model() { ModelName = "RS6", Make = audi });
            context.Models.Add(new Model() { ModelName = "RS7", Make = audi });
            context.Models.Add(new Model() { ModelName = "RS4", Make = audi });
            context.Models.Add(new Model() { ModelName = "RS3", Make = audi });

            context.Models.Add(new Model() { ModelName = "GT", Make = ford });
            context.Models.Add(new Model() { ModelName = "Mondeo", Make = ford });
            context.Models.Add(new Model() { ModelName = "F150", Make = ford });
            context.Models.Add(new Model() { ModelName = "F500", Make = ford });

            context.VehicleFuelTypes.Add(new VehicleFuelType() { FuelType = "Gasoline" });

            var azerbaijan = context.Countries.Add(new Country() { CountryName = "Azerbaijan" }).Entity;
            var poland = context.Countries.Add(new Country() { CountryName = "Poland" }).Entity;
            
            context.Cities.Add(new City() { CityName = "Baku", Country = azerbaijan });
            context.Cities.Add(new City() { CityName = "Quba", Country = azerbaijan });
            context.Cities.Add(new City() { CityName = "Qah", Country = azerbaijan });

            context.Cities.Add(new City() { CityName = "Warsaw", Country = poland });
            context.Cities.Add(new City() { CityName = "Krakow", Country = poland });
            context.Cities.Add(new City() { CityName = "Gdansk", Country = poland });
            
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Alloy Wheels" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "ABS" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Sunroof" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Rain sensor" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Central locking" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Parking sensors" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Air conditioning" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Heated seats" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Leather interior" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Xenon headlights" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Rearview camera" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Side curtains" });
            context.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Seat ventilation" });

            context.VehicleDetailsConditions.Add(new VehicleCondition() 
            { 
                    Condition= "Damaged", 
                    Description= "One or more parts have been replaced or repaired." 
            });
            context.VehicleDetailsConditions.Add(new VehicleCondition()
            {
                Condition= "Painted", 
                Description= "One or more parts have been repainted or cosmetic work has been done."
            });
            context.VehicleDetailsConditions.Add(new VehicleCondition()
            {
                Condition= "Crashed or for parts", 
                Description= "Needs repair or is completely unfit for use."
            });

            context.ManufactureYears.Add(new ManufactureYear() { Year = 2023 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2022 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2021 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2020 });
            context.ManufactureYears.Add(new ManufactureYear() { Year = 2019 });

            var azn = context.Currencies.Add(new Currency() { CurrencyName = "AZN" }).Entity;
            context.Currencies.Add(new Currency() { CurrencyName = "EUR" });
            context.Currencies.Add(new Currency() { CurrencyName = "USD" });

            var subPrice = context.SubscriptionPrices.Add(new SubscriptionPrice() { Price = 5, Currency = azn}).Entity;

            context.Subscriptions.Add(new Subscription() { SubscriptionName = "Premium Account", Price = subPrice });
            context.Subscriptions.Add(new Subscription() { SubscriptionName = "Business Account", Price = subPrice });
            context.AnnouncementPricing.Add(new AnnouncementTypePricing() { PricingName = "Premium Announcement", Price = subPrice });
            context.AnnouncementPricing.Add(new AnnouncementTypePricing() { PricingName = "Regular Announcement", Price = subPrice });
            
            context.AccountLimits.Add(new AccountLimit()
            {
                PremiumAnnouncementsLimit = 0,
                RegularAnnouncementsLimit = 1, 
                UserType = UserType.DefaultAccount
            });
            context.AccountLimits.Add(new AccountLimit()
            {
                PremiumAnnouncementsLimit = 10,
                RegularAnnouncementsLimit = 30,
                UserType = UserType.PremiumAccount
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
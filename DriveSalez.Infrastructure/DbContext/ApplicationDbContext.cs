using System.IdentityModel.Tokens.Jwt;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();

            if (!VehicleColors.Any())
            {
                ApplicationRole defaultAccount  = new ApplicationRole()
                {
                    Name = UserType.DefaultAccount.ToString(),
                    NormalizedName = UserType.DefaultAccount.ToString().ToUpper(),
                };
                
                ApplicationRole premiumAccount = new ApplicationRole()
                {
                    Name = UserType.PremiumAccount.ToString(),
                    NormalizedName = UserType.PremiumAccount.ToString().ToUpper(),
                };

                ApplicationRole businessAccount = new ApplicationRole()
                {
                    Name = UserType.BusinessAccount.ToString(),
                    NormalizedName = UserType.BusinessAccount.ToString().ToUpper(),
                };
                
                Roles.AddRange(defaultAccount, premiumAccount, businessAccount);
                
                VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "USA" });
                VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "Azerbaijan" });
                VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "Korea" });
                VehicleMarketVersions.Add(new VehicleMarketVersion() { MarketVersion = "Europe" });

                VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "White" });
                VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Black" });
                VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Red" });
                VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Yellow" });
                VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Green" });
                VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Blue" });

                VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Sedan" });
                VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Coupe" });
                VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Cabriolet" });
                VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Station Wagon" });
                VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "SUV/Off-Road" });
                VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Pick-Up" });

                VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Manual" });
                VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Auto" });
                VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Robotic" });

                VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "FWD" });
                VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "RWD" });
                VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "AWD" });

                var bmw = Makes.Add(new Make() { MakeName = "BMW" }).Entity;
                var mercedes = Makes.Add(new Make() { MakeName = "Mercedes-Benz" }).Entity;
                var audi = Makes.Add(new Make() { MakeName = "Audi" }).Entity;
                var ford = Makes.Add(new Make() { MakeName = "Ford" }).Entity;

                Models.Add(new Model() { ModelName = "M5", Make = bmw });
                Models.Add(new Model() { ModelName = "M8", Make = bmw });
                Models.Add(new Model() { ModelName = "M3", Make = bmw });
                Models.Add(new Model() { ModelName = "M2", Make = bmw });

                Models.Add(new Model() { ModelName = "C200", Make = mercedes });
                Models.Add(new Model() { ModelName = "C220", Make = mercedes });
                Models.Add(new Model() { ModelName = "C230", Make = mercedes });
                Models.Add(new Model() { ModelName = "C240", Make = mercedes });

                Models.Add(new Model() { ModelName = "RS6", Make = audi });
                Models.Add(new Model() { ModelName = "RS7", Make = audi });
                Models.Add(new Model() { ModelName = "RS4", Make = audi });
                Models.Add(new Model() { ModelName = "RS3", Make = audi });

                Models.Add(new Model() { ModelName = "GT", Make = ford });
                Models.Add(new Model() { ModelName = "Mondeo", Make = ford });
                Models.Add(new Model() { ModelName = "F150", Make = ford });
                Models.Add(new Model() { ModelName = "F500", Make = ford });

                VehicleFuelTypes.Add(new VehicleFuelType() { FuelType = "Gasoline" });

                var azerbaijan = Countries.Add(new Country() { CountryName = "Azerbaijan" }).Entity;
                var poland = Countries.Add(new Country() { CountryName = "Poland" }).Entity;
                
                Cities.Add(new City() { CityName = "Baku", Country = azerbaijan });
                Cities.Add(new City() { CityName = "Quba", Country = azerbaijan });
                Cities.Add(new City() { CityName = "Qah", Country = azerbaijan });

                Cities.Add(new City() { CityName = "Warsaw", Country = poland });
                Cities.Add(new City() { CityName = "Krakow", Country = poland });
                Cities.Add(new City() { CityName = "Gdansk", Country = poland });
                
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Alloy Wheels" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "ABS" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Sunroof" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Rain sensor" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Central locking" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Parking sensors" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Air conditioning" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Heated seats" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Leather interior" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Xenon headlights" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Rearview camera" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Side curtains" });
                VehicleDetailsOptions.Add(new VehicleOption() { Option = "Seat ventilation" });

                VehicleDetailsConditions.Add(new VehicleCondition() 
                { 
                        Condition= "Damaged", 
                        Description= "One or more parts have been replaced or repaired." 
                });
                VehicleDetailsConditions.Add(new VehicleCondition()
                {
                    Condition= "Painted", 
                    Description= "One or more parts have been repainted or cosmetic work has been done."
                });
                VehicleDetailsConditions.Add(new VehicleCondition()
                {
                    Condition= "Crashed or for parts", 
                    Description= "Needs repair or is completely unfit for use."
                });

                ManufactureYears.Add(new ManufactureYear() { Year = 2023 });
                ManufactureYears.Add(new ManufactureYear() { Year = 2022 });
                ManufactureYears.Add(new ManufactureYear() { Year = 2021 });
                ManufactureYears.Add(new ManufactureYear() { Year = 2020 });
                ManufactureYears.Add(new ManufactureYear() { Year = 2019 });

                var azn = Currencies.Add(new Currency() { CurrencyName = "AZN" }).Entity;
                Currencies.Add(new Currency() { CurrencyName = "EUR" });
                Currencies.Add(new Currency() { CurrencyName = "USD" });

                var subPrice = SubscriptionPrices.Add(new SubscriptionPrice() { Price = 5, Currency = azn}).Entity;

                Subscriptions.Add(new Subscription() { SubscriptionName = "Premium", Price = subPrice });
                
                AccountLimits.Add(new AccountLimit()
                {
                    PremiumAnnouncementsLimit = 0,
                    RegularAnnouncementsLimit = 1, 
                    UserType = UserType.DefaultAccount
                });
                AccountLimits.Add(new AccountLimit()
                {
                    PremiumAnnouncementsLimit = 10,
                    RegularAnnouncementsLimit = 30,
                    UserType = UserType.PremiumAccount
                });
                AccountLimits.Add(new AccountLimit()
                {
                    PremiumAnnouncementsLimit = 20, 
                    RegularAnnouncementsLimit = 50, 
                    UserType = UserType.BusinessAccount
                });
            }
        }
        
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        public DbSet<PaidUser> PaidUsers => Set<PaidUser>();

        public DbSet<DefaultAccount> DefaultAccounts => Set<DefaultAccount>();

        public DbSet<BusinessAccount> BusinessAccounts => Set<BusinessAccount>();

        public DbSet<PremiumAccount> PremiumAccounts => Set<PremiumAccount>();
        
        public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();

        public DbSet<ImageUrl> ImageUrls => Set<ImageUrl>();
        
        public DbSet<AccountPhoneNumber> AccountPhoneNumbers => Set<AccountPhoneNumber>();

        public DbSet<AccountLimit> AccountLimits => Set<AccountLimit>();

        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        
        public DbSet<SubscriptionPrice> SubscriptionPrices => Set<SubscriptionPrice>();
        
        public DbSet<Announcement> Announcements => Set<Announcement>();

        public DbSet<Make> Makes => Set<Make>();

        public DbSet<Model> Models => Set<Model>();

        public DbSet<Vehicle> Vehicles => Set<Vehicle>(); 

        public DbSet<VehicleDetails> VehicleDetails => Set<VehicleDetails>(); 

        public DbSet<VehicleBodyType> VehicleBodyTypes => Set<VehicleBodyType>();

        public DbSet<VehicleColor> VehicleColors => Set<VehicleColor>();

        public DbSet<VehicleGearboxType> VehicleGearboxTypes => Set<VehicleGearboxType>();

        public DbSet<VehicleDrivetrainType> VehicleDriveTrainTypes => Set<VehicleDrivetrainType>();

        public DbSet<VehicleMarketVersion> VehicleMarketVersions => Set<VehicleMarketVersion>();

        public DbSet<VehicleFuelType> VehicleFuelTypes => Set<VehicleFuelType>();

        public DbSet<VehicleCondition> VehicleDetailsConditions => Set<VehicleCondition>();

        public DbSet<VehicleOption> VehicleDetailsOptions=> Set<VehicleOption>();

        public DbSet<Country> Countries => Set<Country>();
        
        public DbSet<City> Cities => Set<City>();

        public DbSet<ManufactureYear> ManufactureYears => Set<ManufactureYear>();

        public DbSet<Currency> Currencies => Set<Currency>();
    }
}

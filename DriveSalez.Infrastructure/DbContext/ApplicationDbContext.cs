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
                ApplicationRole applicationRole1 = new ApplicationRole()
                {
                    Name = UserType.DefaultAccount.ToString(),
                    NormalizedName = UserType.DefaultAccount.ToString().ToUpper(),
                };
                
                ApplicationRole applicationRole2 = new ApplicationRole()
                {
                    Name = UserType.PremiumAccount.ToString(),
                    NormalizedName = UserType.PremiumAccount.ToString().ToUpper(),
                };

                ApplicationRole applicationRole3 = new ApplicationRole()
                {
                    Name = UserType.BusinessAccount.ToString(),
                    NormalizedName = UserType.BusinessAccount.ToString().ToUpper(),
                };
                
                Roles.AddRange(applicationRole1, applicationRole2, applicationRole3);
                
                var marketVersion = this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion()
                    { MarketVersion = "USA" }).Entity;
                this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion()
                    { MarketVersion = "Azerbaijan" });
                this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion()
                    { MarketVersion = "Korea" });
                this.VehicleMarketVersions.Add(
                    new Core.Entities.VehicleParts.VehicleMarketVersion() { MarketVersion = "Europe" });

                var white = this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "White" })
                    .Entity;
                this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Black" });
                this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Red" });
                this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Yellow" });
                this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Green" });
                this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Color = "Blue" });

                var sedan = this.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Sedan" }).Entity;
                this.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Coupe" });
                this.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Cabriolet" });
                this.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Station Wagon" });
                this.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "SUV/Off-Road" });
                this.VehicleBodyTypes.Add(new VehicleBodyType() { BodyType = "Pick-Up" });

                var gearboxType = this.VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Manual" }).Entity;
                this.VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Auto" });
                this.VehicleGearboxTypes.Add(new VehicleGearboxType() { GearboxType = "Robotic" });

                var fwd = this.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "FWD" }).Entity;
                this.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "RWD" });
                this.VehicleDriveTrainTypes.Add(new VehicleDrivetrainType() { DrivetrainType = "AWD" });

                var bmw = this.Makes.Add(new Make() { MakeName = "BMW" }).Entity;
                var mercedes = this.Makes.Add(new Make() { MakeName = "Mercedes-Benz" }).Entity;
                var audi = this.Makes.Add(new Make() { MakeName = "Audi" }).Entity;
                var ford = this.Makes.Add(new Make() { MakeName = "Ford" }).Entity;

                this.Models.Add(new Model() { ModelName = "M5", Make = bmw });
                this.Models.Add(new Model() { ModelName = "M8", Make = bmw });
                this.Models.Add(new Model() { ModelName = "M3", Make = bmw });
                this.Models.Add(new Model() { ModelName = "M2", Make = bmw });

                this.Models.Add(new Model() { ModelName = "C200", Make = mercedes });
                var merc_model = this.Models.Add(new Model() { ModelName = "C220", Make = mercedes }).Entity;
                this.Models.Add(new Model() { ModelName = "C230", Make = mercedes });
                this.Models.Add(new Model() { ModelName = "C240", Make = mercedes });

                this.Models.Add(new Model() { ModelName = "RS6", Make = audi });
                this.Models.Add(new Model() { ModelName = "RS7", Make = audi });
                this.Models.Add(new Model() { ModelName = "RS4", Make = audi });
                this.Models.Add(new Model() { ModelName = "RS3", Make = audi });

                this.Models.Add(new Model() { ModelName = "GT", Make = ford });
                this.Models.Add(new Model() { ModelName = "Mondeo", Make = ford });
                this.Models.Add(new Model() { ModelName = "F150", Make = ford });
                this.Models.Add(new Model() { ModelName = "F500", Make = ford });

                var gasoline = this.VehicleFuelTypes.Add(new VehicleFuelType() { FuelType = "Gasoline" }).Entity;

                var aze = this.Countries.Add(new Country() { CountryName = "Azerbaijan" }).Entity;
                var pl = this.Countries.Add(new Country() { CountryName = "Poland" }).Entity;
                
                this.Cities.Add(new City() { CityName = "Baku", Country = aze });
                this.Cities.Add(new City() { CityName = "Quba", Country = aze });
                this.Cities.Add(new City() { CityName = "Qax", Country = aze });

                this.Cities.Add(new City() { CityName = "Warsaw", Country = pl });
                this.Cities.Add(new City() { CityName = "Krakow", Country = pl });
                this.Cities.Add(new City() { CityName = "Gdansk", Country = pl });
                
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Alloy Wheels" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "ABS" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Sunroof" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Rain sensor" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Central locking" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Parking sensors" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Air conditioning" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Heated seats" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Leather interior" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Xenon headlights" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Rearview camera" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Side curtains" });
                this.VehicleDetailsOptions.Add(new VehicleOption() { Option = "Seat ventilation" });

                this.VehicleDetailsConditions.Add(new VehicleCondition() { Condition= "Damaged", Description= "One or more parts have been replaced or repaired." });
                this.VehicleDetailsConditions.Add(new VehicleCondition() { Condition= "Painted", Description= "One or more parts have been repainted or cosmetic work has been done." });
                this.VehicleDetailsConditions.Add(new VehicleCondition() { Condition= "Crashed or for parts", Description= "Needs repair or is completely unfit for use." });

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
                
                PaidAccountLimits.Add(new PaidAccountLimit() { PremiumAnnouncementsLimit = 10, UserType = UserType.PremiumAccount});
                PaidAccountLimits.Add(new PaidAccountLimit() { PremiumAnnouncementsLimit = 20, UserType = UserType.BusinessAccount});
            }
        }
        
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //
        //     modelBuilder.Entity<DefaultAccount>().ToTable("DefaultAccounts");
        //     modelBuilder.Entity<PremiumAccount>().ToTable("PremiumAccounts");
        //     modelBuilder.Entity<BusinessAccount>().ToTable("BusinessAccounts");
        // }

        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        public DbSet<PaidUser> PaidUsers => Set<PaidUser>();

        public DbSet<DefaultAccount> DefaultAccounts => Set<DefaultAccount>();

        public DbSet<BusinessAccount> BusinessAccounts => Set<BusinessAccount>();

        public DbSet<PremiumAccount> PremiumAccounts => Set<PremiumAccount>();
        
        public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();

        public DbSet<AccountPhoneNumber> AccountPhoneNumbers => Set<AccountPhoneNumber>();

        public DbSet<PaidAccountLimit> PaidAccountLimits => Set<PaidAccountLimit>();

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

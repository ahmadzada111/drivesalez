using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();

            var marketVersion = this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion() { Name = "USA" }).Entity;
            this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion() { Name = "Azerbaijan" });
            this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion() { Name = "Korea" });
            this.VehicleMarketVersions.Add(new Core.Entities.VehicleParts.VehicleMarketVersion() { Name = "Europe" });

            var white = this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Name = "White" }).Entity;
            this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Name = "Black" });
            this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Name = "Red" });
            this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Name = "Yellow" });
            this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Name = "Green" });
            this.VehicleColors.Add(new Core.Entities.VehicleParts.VehicleColor() { Name = "Blue" });

            var sedan = this.VehicleBodyTypes.Add(new VehicleBodyType() { Name = "Sedan" }).Entity;
            this.VehicleBodyTypes.Add(new VehicleBodyType() { Name = "Coupe" });
            this.VehicleBodyTypes.Add(new VehicleBodyType() { Name = "Cabriolet" });
            this.VehicleBodyTypes.Add(new VehicleBodyType() { Name = "Station Wagon" });
            this.VehicleBodyTypes.Add(new VehicleBodyType() { Name = "SUV/Off-Road" });
            this.VehicleBodyTypes.Add(new VehicleBodyType() { Name = "Pick-Up" });

            var gearboxType = this.VehicleGearboxTypes.Add(new VehicleGearboxType() { Name = "Manual" }).Entity;
            this.VehicleGearboxTypes.Add(new VehicleGearboxType() { Name = "Auto" });
            this.VehicleGearboxTypes.Add(new VehicleGearboxType() { Name = "Robotic" });

            var fwd = this.VehicleDriveTrainTypes.Add(new VehicleDriveTrainType() { Name = "FWD" }).Entity;
            this.VehicleDriveTrainTypes.Add(new VehicleDriveTrainType() { Name = "RWD" });
            this.VehicleDriveTrainTypes.Add(new VehicleDriveTrainType() { Name = "AWD" });

            var bmw = this.Makes.Add(new Make() { Name = "BMW" }).Entity;
            var mercedes = this.Makes.Add(new Make() { Name = "Mercedes-Benz" }).Entity;
            var audi = this.Makes.Add(new Make() { Name = "Audi" }).Entity;
            var ford = this.Makes.Add(new Make() { Name = "Ford" }).Entity;

            this.Models.Add(new Model() { Name = "M5", Make = bmw });
            this.Models.Add(new Model() { Name = "M8", Make = bmw });
            this.Models.Add(new Model() { Name = "M3", Make = bmw });
            this.Models.Add(new Model() { Name = "M2", Make = bmw });

            this.Models.Add(new Model() { Name = "C200", Make = mercedes });
            var merc_model = this.Models.Add(new Model() { Name = "C220", Make = mercedes }).Entity;
            this.Models.Add(new Model() { Name = "C230", Make = mercedes });
            this.Models.Add(new Model() { Name = "C240", Make = mercedes });

            this.Models.Add(new Model() { Name = "RS6", Make = audi });
            this.Models.Add(new Model() { Name = "RS7", Make = audi });
            this.Models.Add(new Model() { Name = "RS4", Make = audi });
            this.Models.Add(new Model() { Name = "RS3", Make = audi });

            this.Models.Add(new Model() { Name = "GT", Make = ford });
            this.Models.Add(new Model() { Name = "Mondeo", Make = ford });
            this.Models.Add(new Model() { Name = "F150", Make = ford });
            this.Models.Add(new Model() { Name = "F500", Make = ford });

            var gasoline = this.VehicleFuelTypes.Add(new VehicleFuelType() { FuelType = "Gasoline" }).Entity;

            var aze = this.Countries.Add(new Country() {Name="Azerbaijan" }).Entity;

            this.Cities.Add(new City() { Name = "Baku", Country = aze });
            this.Cities.Add(new City() { Name="Quba",Country=aze});
            this.Cities.Add(new City() { Name = "Qax", Country = aze });
        }
        
        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

        public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();

        public DbSet<Announcement> Announcements => Set<Announcement>();
        
        public DbSet<CarDealer> CarDealers => Set<CarDealer>();

        public DbSet<Make> Makes => Set<Make>();

        public DbSet<Model> Models => Set<Model>();

        public DbSet<Vehicle> Vehicles => Set<Vehicle>(); 

        public DbSet<VehicleDetails> VehicleDetails => Set<VehicleDetails>(); 

        public DbSet<VehicleBodyType> VehicleBodyTypes => Set<VehicleBodyType>();

        public DbSet<VehicleColor> VehicleColors => Set<VehicleColor>();

        public DbSet<VehicleGearboxType> VehicleGearboxTypes => Set<VehicleGearboxType>();

        public DbSet<VehicleDriveTrainType> VehicleDriveTrainTypes => Set<VehicleDriveTrainType>();

        public DbSet<VehicleMarketVersion> VehicleMarketVersions => Set<VehicleMarketVersion>();

        public DbSet<VehicleFuelType> VehicleFuelTypes => Set<VehicleFuelType>();

        public DbSet<VehicleDetailsCondition> VehicleDetailsConditions => Set<VehicleDetailsCondition>();

        public DbSet<VehicleDetailsOptions> VehicleDetailsOptions=> Set<VehicleDetailsOptions>();

        public DbSet<Country> Countries => Set<Country>();
        
        public DbSet<City> Cities => Set<City>();
    }
}

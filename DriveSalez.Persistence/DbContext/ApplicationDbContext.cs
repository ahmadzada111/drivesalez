using System.Reflection;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.DbContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public virtual DbSet<ImageUrl> ImageUrls { get; set; }
        
    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public virtual DbSet<AccountLimit> AccountLimits { get; set; }

    public virtual DbSet<PricingOption> PricingOptions { get; set; }
                
    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; } 

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }

    public virtual DbSet<BodyType> BodyTypes { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<GearboxType> GearboxTypes { get; set; }

    public virtual DbSet<DrivetrainType> DrivetrainTypes { get; set; }

    public virtual DbSet<MarketVersion> MarketVersions { get; set; }

    public virtual DbSet<FuelType> FuelTypes { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Country> Countries { get; set; }
        
    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ManufactureYear> ManufactureYears { get; set; }
}
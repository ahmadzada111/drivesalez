using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Persistence.Configuration;
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

        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new BusinessAccountConfiguration());
        modelBuilder.ApplyConfiguration(new ImageUrlConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
        modelBuilder.ApplyConfiguration(new AccountLimitConfiguration());
        modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());
        modelBuilder.ApplyConfiguration(new AnnouncementTypePricingConfiguration());
        modelBuilder.ApplyConfiguration(new BodyTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new ConditionConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new DefaultAccountConfiguration());
        modelBuilder.ApplyConfiguration(new DrivetrainTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FuelTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GearboxTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageUrlConfiguration());
        modelBuilder.ApplyConfiguration(new ManufactureYearConfiguration());
        modelBuilder.ApplyConfiguration(new MarketVersionConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
        modelBuilder.ApplyConfiguration(new PriceDetailConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleDetailConfiguration());
    }

    public virtual DbSet<ImageUrl> ImageUrls { get; set; }
        
    public virtual DbSet<PhoneNumber> AccountPhoneNumbers { get; set; }

    public virtual DbSet<AccountLimit> AccountLimits { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<AnnouncementTypePricing> AnnouncementPricing { get; set; }
        
    public virtual DbSet<PriceDetail> SubscriptionPrices { get; set; }
        
    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; } 

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }

    public virtual DbSet<BodyType> VehicleBodyTypes { get; set; }

    public virtual DbSet<Color> VehicleColors { get; set; }

    public virtual DbSet<GearboxType> VehicleGearboxTypes { get; set; }

    public virtual DbSet<DrivetrainType> VehicleDriveTrainTypes { get; set; }

    public virtual DbSet<MarketVersion> VehicleMarketVersions { get; set; }

    public virtual DbSet<FuelType> VehicleFuelTypes { get; set; }

    public virtual DbSet<Condition> VehicleDetailsConditions { get; set; }

    public virtual DbSet<Option> VehicleDetailsOptions { get; set; }

    public virtual DbSet<Country> Countries { get; set; }
        
    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ManufactureYear> ManufactureYears { get; set; }
}
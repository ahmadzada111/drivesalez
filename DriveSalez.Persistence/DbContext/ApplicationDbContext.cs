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

    public virtual DbSet<ImageUrl> ImageUrls => Set<ImageUrl>();
        
    public virtual DbSet<AccountPhoneNumber> AccountPhoneNumbers => Set<AccountPhoneNumber>();

    public virtual DbSet<AccountLimit> AccountLimits => Set<AccountLimit>();

    public virtual DbSet<Subscription> Subscriptions => Set<Subscription>();

    public virtual DbSet<AnnouncementTypePricing> AnnouncementPricing => Set<AnnouncementTypePricing>();
        
    public virtual DbSet<SubscriptionPrice> SubscriptionPrices => Set<SubscriptionPrice>();
        
    public virtual DbSet<Announcement> Announcements => Set<Announcement>();

    public virtual DbSet<Make> Makes => Set<Make>();

    public virtual DbSet<Model> Models => Set<Model>();

    public virtual DbSet<Vehicle> Vehicles => Set<Vehicle>(); 

    public virtual DbSet<VehicleDetails> VehicleDetails => Set<VehicleDetails>(); 

    public virtual DbSet<VehicleBodyType> VehicleBodyTypes => Set<VehicleBodyType>();

    public virtual DbSet<VehicleColor> VehicleColors => Set<VehicleColor>();

    public virtual DbSet<VehicleGearboxType> VehicleGearboxTypes => Set<VehicleGearboxType>();

    public virtual DbSet<VehicleDrivetrainType> VehicleDriveTrainTypes => Set<VehicleDrivetrainType>();

    public virtual DbSet<VehicleMarketVersion> VehicleMarketVersions => Set<VehicleMarketVersion>();

    public virtual DbSet<VehicleFuelType> VehicleFuelTypes => Set<VehicleFuelType>();

    public virtual DbSet<VehicleCondition> VehicleDetailsConditions => Set<VehicleCondition>();

    public virtual DbSet<VehicleOption> VehicleDetailsOptions=> Set<VehicleOption>();

    public virtual DbSet<Country> Countries => Set<Country>();
        
    public virtual DbSet<City> Cities => Set<City>();

    public virtual DbSet<ManufactureYear> ManufactureYears => Set<ManufactureYear>();

    public virtual DbSet<Currency> Currencies => Set<Currency>();
}
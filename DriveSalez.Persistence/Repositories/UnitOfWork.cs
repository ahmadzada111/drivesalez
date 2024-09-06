using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IColorRepository Colors { get; }
    public IConditionRepository Conditions { get; }
    public IMarketVersionRepository MarketVersions { get; }
    public ICityRepository Cities { get; }
    public ICountryRepository Countries { get; }
    public IBodyTypeRepository BodyTypes { get; }
    public IOptionRepository Options { get; } 
    public IMakeRepository Makes { get; }
    public IModelRepository Models { get; }
    public IFuelTypeRepository FuelTypes { get; }
    public IGearboxTypeRepository GearboxTypes { get; }
    public IDrivetrainTypeRepository DrivetrainTypes { get; }
    public IManufactureYearRepository ManufactureYears { get; }
    public IAnnouncementRepository Announcements { get; }
    public IUserRepository Users { get; }
    public IImageUrlRepository ImageUrls { get; }
    public IOneTimePurchaseRepository OneTimePurchases { get; }
    public IPhoneNumberRepository PhoneNumbers { get; }
    public ISubscriptionRepository Subscriptions { get; }
    public IUserLimitRepository UserLimits { get; }
    public IUserPurchaseRepository UserPurchases { get; }
    public IUserSubscriptionRepository UserSubscriptions { get; }
    public IVehicleRepository Vehicles { get; }
    public IVehicleDetailRepository VehicleDetails { get; }

    public UnitOfWork(ApplicationDbContext dbContext, 
        IColorRepository colors, IConditionRepository conditions,
        IMarketVersionRepository marketVersions, ICityRepository cities, 
        ICountryRepository countries, IBodyTypeRepository bodyTypes, 
        IOptionRepository options, IMakeRepository makes, 
        IModelRepository models, IFuelTypeRepository fuelTypes, 
        IGearboxTypeRepository gearboxTypes, IDrivetrainTypeRepository drivetrainTypes, 
        IManufactureYearRepository manufactureYears, IAnnouncementRepository announcements, 
        IUserRepository users, IImageUrlRepository imageUrls, IOneTimePurchaseRepository oneTimePurchases, 
        IPhoneNumberRepository phoneNumbers, ISubscriptionRepository subscriptions, 
        IUserLimitRepository userLimits, IUserPurchaseRepository userPurchases, 
        IUserSubscriptionRepository userSubscriptions, IVehicleRepository vehicles, 
        IVehicleDetailRepository vehicleDetails)
    {
        _dbContext = dbContext;
        Colors = colors;
        Conditions = conditions;
        MarketVersions = marketVersions;
        Cities = cities;
        Countries = countries;
        BodyTypes = bodyTypes;
        Options = options;
        Makes = makes;
        Models = models;
        FuelTypes = fuelTypes;
        GearboxTypes = gearboxTypes;
        DrivetrainTypes = drivetrainTypes;
        ManufactureYears = manufactureYears;
        Announcements = announcements;
        Users = users;
        ImageUrls = imageUrls;
        OneTimePurchases = oneTimePurchases;
        PhoneNumbers = phoneNumbers;
        Subscriptions = subscriptions;
        UserLimits = userLimits;
        UserPurchases = userPurchases;
        UserSubscriptions = userSubscriptions;
        Vehicles = vehicles;
        VehicleDetails = vehicleDetails;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
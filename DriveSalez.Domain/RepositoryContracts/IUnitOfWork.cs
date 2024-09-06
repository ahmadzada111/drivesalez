namespace DriveSalez.Domain.RepositoryContracts;

public interface IUnitOfWork : IDisposable
{
    IColorRepository Colors { get; }

    IConditionRepository Conditions { get; }
    
    IMarketVersionRepository MarketVersions { get; }

    ICityRepository Cities { get; }

    ICountryRepository Countries { get; }
    
    IBodyTypeRepository BodyTypes { get; }

    IOptionRepository Options { get; } 
    
    IMakeRepository Makes { get; }
    
    IModelRepository Models { get; }
    
    IFuelTypeRepository FuelTypes { get; }

    IGearboxTypeRepository GearboxTypes { get; }
    
    IDrivetrainTypeRepository DrivetrainTypes { get; }
    
    IManufactureYearRepository ManufactureYears { get; }
    
    IAnnouncementRepository Announcements { get; }

    IUserRepository Users { get; }
    
    IImageUrlRepository ImageUrls { get; }
    
    IOneTimePurchaseRepository OneTimePurchases { get; }
    
    IPhoneNumberRepository PhoneNumbers { get; }
    
    ISubscriptionRepository Subscriptions { get; }
    
    IUserLimitRepository UserLimits { get; }
    
    IUserPurchaseRepository UserPurchases { get; }
    
    IUserSubscriptionRepository UserSubscriptions { get; }
    
    IVehicleRepository Vehicles { get; }
    
    IVehicleDetailRepository VehicleDetails { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
        
    public AdminRepository(ApplicationDbContext dbContext, ILogger<AdminRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Color> SendNewColorToDbAsync(string color)
    {
        try
        {
            _logger.LogInformation($"Sending new color to DB");

            ArgumentException.ThrowIfNullOrEmpty(color);

            var response = await _dbContext.Colors.AddAsync(new Color() { Title = color });

            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending new color to DB");
            throw;
        }
    }

    public async Task<Condition> SendNewVehicleConditionToDbAsync(string condition, string description)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle condition to DB");

            ArgumentException.ThrowIfNullOrEmpty(condition);

            var response = await _dbContext.Conditions.AddAsync(new Condition()
            {
                Title = condition, 
                Description = description
            });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle condition to DB");
            throw;
        }
    }

    public async Task<MarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle market version to DB");

            ArgumentException.ThrowIfNullOrEmpty(marketVersion);

            var response = await _dbContext.MarketVersions.AddAsync(new MarketVersion() { Version = marketVersion });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle market version to DB");
            throw;
        }
    }

    public async Task<Option> SendNewVehicleOptionToDbAsync(string option)
    {
        try
        {
            _logger.LogInformation("Sending new vehicle option to DB");

            ArgumentException.ThrowIfNullOrEmpty(option);

            var response = await _dbContext.Options.AddAsync(new Option() { Title = option });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new vehicle option to DB");
            throw;
        }
    }

    //CHECK
    public async Task<PricingOption> SendNewSubscriptionToDbAsync(string subscriptionName, decimal price)
    {
        try
        {
            _logger.LogInformation("Sending new subscription to DB");

            ArgumentException.ThrowIfNullOrEmpty(subscriptionName);

            var response = await _dbContext.PricingOptions.AddAsync(new PricingOption()
            {
                Title = subscriptionName,
                Price = price
            });
            
            if (response.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return response.Entity;
            }

            throw new InvalidOperationException("Object wasn't added");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending new subscription to DB");
            throw;
        }
    }

    public async Task<Color> UpdateVehicleColorInDbAsync(int colorId, string newColor)
    {
        try
        {
            _logger.LogInformation($"Updating color with ID {colorId} to new value {newColor} in DB");

            ArgumentException.ThrowIfNullOrEmpty(newColor);

            var color = await _dbContext.FindAsync<Color>(colorId) ?? new Color();
            color.Title = newColor;
            
            var response = _dbContext.Update(color);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return color;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating color with ID {colorId} to new value {newColor} in DB");
            throw;
        }
    }

    //CHECK
    public async Task<PricingOption> UpdateSubscriptionInDbAsync(int subscriptionId, decimal price)
    {
        try
        {
            _logger.LogInformation($"Updating subscription with ID {subscriptionId} in DB");
                
            var subscription = await _dbContext.FindAsync<PricingOption>(subscriptionId) ??
            throw new InvalidOperationException($"Subscription with ID {subscriptionId} does not exist");

            subscription.Price = price;
            
            var response = _dbContext.Update(subscription);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return subscription;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating subscription with ID {subscriptionId} in DB");
            throw;
        }
    }

    public async Task<AccountLimit> UpdateAccountLimitInDbAsync(int limitId, int premiumLimit, int regularLimit)
    {
        try
        {
            if (premiumLimit < 0 || regularLimit < 0)
            {
                throw new ArgumentException();
            }

            var accountLimit = await _dbContext.FindAsync<AccountLimit>(limitId) ??
            throw new InvalidOperationException($"Limit with ID {limitId} does not exist");

            accountLimit.PremiumAnnouncementsLimit = premiumLimit;
            accountLimit.RegularAnnouncementsLimit = regularLimit;
            
            var response = _dbContext.Update(accountLimit);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return accountLimit;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating account limit with ID {limitId}");
            throw;
        }
    }
        
    public async Task<Condition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition, string newDescription)
    {
        try
        {
            if (string.IsNullOrEmpty(newVehicleCondition) || string.IsNullOrEmpty(newDescription))
            {
                throw new ArgumentException();
            }

            var vehicleCondition = await _dbContext.FindAsync<Condition>(vehicleConditionId) ??
            throw new InvalidOperationException($"Condition with ID {vehicleConditionId} does not exist");

            vehicleCondition.Title = newVehicleCondition;
            vehicleCondition.Description = newDescription;
            
            var response = _dbContext.Update(vehicleCondition);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleCondition;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating vehicle condition type with ID {vehicleConditionId} to new value {newVehicleCondition}");
            throw;
        }
    }
        
    public async Task<Option> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newVehicleOption);

            var vehicleOption = await _dbContext.FindAsync<Option>(vehicleOptionId) ??
            throw new InvalidOperationException($"Option with ID {vehicleOptionId} does not exist");

            vehicleOption.Title = newVehicleOption;
            var response = _dbContext.Update(vehicleOption);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleOption;   
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating vehicle option type with ID {vehicleOptionId} to new value {newVehicleOption}");
            throw;
        }
    }
        
    public async Task<MarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(newMarketVersion);

            var marketVersion = await _dbContext.FindAsync<MarketVersion>(marketVersionId) ?? 
            throw new InvalidOperationException($"Market version with ID {marketVersionId} does not exist");

            marketVersion.Version = newMarketVersion;
            var response = _dbContext.Update(marketVersion);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return marketVersion;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating market version type with ID {marketVersionId} to new value {newMarketVersion}");
            throw;
        }
    }

    public async Task<Color?> DeleteVehicleColorFromDbAsync(int colorId)
    {
        try
        {
            var color = await _dbContext.FindAsync<Color>(colorId);
            
            if (color != null)
            {
                var response = _dbContext.Remove(color);

                if (response.State == EntityState.Deleted)
                {
                    await _dbContext.SaveChangesAsync();
                    return color;
                }
                
                throw new InvalidOperationException("Object wasn't deleted");
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<BodyType?> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId)
    {
        var bodyType = await _dbContext.FindAsync<BodyType>(bodyTypeId);
            
        if (bodyType != null)
        {
            var response = _dbContext.Remove(bodyType);
                
            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return bodyType;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Country?> DeleteCountryFromDbAsync(int countryId)
    {
        var country = await _dbContext.FindAsync<Country>(countryId);
            
        if (country != null)
        {
            var cities = await _dbContext.Cities
                .Where(x => x.Country.Id == country.Id)
                .ToListAsync();
                
            _dbContext.Cities.RemoveRange(cities);
            var response = _dbContext.Remove(country);
                
            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return country;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<DrivetrainType?> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId)
    {
        var drivetrain = await _dbContext.FindAsync<DrivetrainType>(driveTrainId);
            
        if (drivetrain != null)
        {
            var response = _dbContext.Remove(drivetrain);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return drivetrain;   
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<GearboxType?> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId)
    {
        var gearbox = await _dbContext.FindAsync<GearboxType>(gearboxId);
            
        if (gearbox != null)
        {
            var response = _dbContext.Remove(gearbox);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return gearbox;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    //CHECK
    public async Task<PricingOption?> DeleteSubscriptionFromDbAsync(int subscriptionId)
    {
        var subscription = await _dbContext.FindAsync<PricingOption>(subscriptionId);
            
        if (subscription != null)
        {
            var response = _dbContext.Remove(subscription);
                
            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return subscription;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Make?> DeleteMakeFromDbAsync(int makeId)
    {
        var make = await _dbContext.FindAsync<Make>(makeId);
            
        if (make != null)
        {
            var response = _dbContext.Remove(make);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return make;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<City?> DeleteCityFromDbAsync(int cityId)
    {
        var city = await _dbContext.FindAsync<City>(cityId);
            
        if (city != null)
        {
            var response = _dbContext.Remove(city);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return city;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }
        
    public async Task<Model?> DeleteModelFromDbAsync(int modelId)
    {
        var model = await _dbContext.FindAsync<Model>(modelId);
            
        if (model != null)
        {
            var response = _dbContext.Remove(model);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return model;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<FuelType?> DeleteFuelTypeFromDbAsync(int fuelTypeId)
    {
        var fuelType = await _dbContext.FindAsync<FuelType>(fuelTypeId);
            
        if (fuelType != null)
        {
            var response = _dbContext.Remove(fuelType);
                
            if(response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return fuelType;   
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Condition?> DeleteVehicleConditionFromDbAsync(int vehicleConditionId)
    {
        var vehicleCondition = await _dbContext.FindAsync<Condition>(vehicleConditionId);
            
        if (vehicleCondition != null)
        {
            var response = _dbContext.Remove(vehicleCondition);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleCondition;   
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<Option?> DeleteVehicleOptionFromDbAsync(int vehicleOptionId)
    {
        var vehicleOption = await _dbContext.FindAsync<Option>(vehicleOptionId);
            
        if (vehicleOption != null)
        {
            var response = _dbContext.Remove(vehicleOption);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return vehicleOption;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<MarketVersion?> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId)
    {
        var marketVersion = await _dbContext.FindAsync<MarketVersion>(marketVersionId);
            
        if (marketVersion != null)
        {
            var response = _dbContext.Remove(marketVersion);

            if (response.State == EntityState.Deleted)
            {
                await _dbContext.SaveChangesAsync();
                return marketVersion;
            }
                
            throw new InvalidOperationException("Object wasn't deleted");
        }
            
        return null;
    }

    public async Task<ApplicationUser?> DeleteModeratorFromDbAsync(Guid moderatorId)
    {
        var moderator = await _dbContext.Users
            .Where(x => x.Id == moderatorId)
            .FirstOrDefaultAsync();

        if (moderator == null)
        {
            return null;
        }
            
        var response = _dbContext.Remove(moderatorId);
            
        if (response.State == EntityState.Deleted)
        {
            await _dbContext.SaveChangesAsync();
            return moderator;
        }
            
        throw new InvalidOperationException("Object wasn't deleted");
    }

    public async Task<PaginatedList<ApplicationUser>> GetAllUsersFromDbAsync(PagingParameters pagingParameters)
    {
        try
        {
            _logger.LogError($"Getting all users from db");

            var query = _dbContext.Users
                .Where(x => x.EmailConfirmed)
                .Join(_dbContext.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new
                    {
                        User = user, UserRole = userRole
                    })
                .Where(joined => !_dbContext.Roles
                    .Any(r => r.Name == UserType.Admin.ToString() || r.Name == UserType.Moderator.ToString()))
                .Select(joined => joined.User)
                .Include(u => u.PhoneNumbers);
            
            var totalCount = await query.CountAsync();
            var users = await query
                    .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
                    .Take(pagingParameters.PageSize)
                    .ToListAsync();
            
            if (users.IsNullOrEmpty())
            {
                return new PaginatedList<ApplicationUser>();
            }

            var paginatedUsers = PaginatedList<ApplicationUser>.Create(users, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);

            return paginatedUsers;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting all users from db");
            throw;
        }
    }

    public async Task<bool> BanUserInDbAsync(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        if (!user.IsBanned)
        {
            user.IsBanned = true;

            var result = _dbContext.Update(user);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }

        return false;
    }

    public async Task<bool> UnbanUserInDbAsync(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return false;
        }

        if (user.IsBanned)
        {
            user.IsBanned = false;

            var result = _dbContext.Update(user);

            if (result.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new InvalidOperationException("Object wasn't modified");
        }

        return false;
    }
}
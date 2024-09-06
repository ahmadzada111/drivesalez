using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class UserPurchase
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public UserProfile User { get; set; }

    public int OneTimePurchaseId { get; set; }

    public OneTimePurchase OneTimePurchase { get; set; }

    public DateTimeOffset PurchaseDate { get; set; }
}
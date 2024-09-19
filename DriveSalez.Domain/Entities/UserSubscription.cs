using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class UserSubscription
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public int SubscriptionId { get; set; }

    public Subscription Subscription { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }
}
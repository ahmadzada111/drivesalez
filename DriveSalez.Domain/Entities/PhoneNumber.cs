using System.Text.Json.Serialization;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }

    public string Number { get; set; }

    public Guid UserId { get; set; }

    public BusinessAccount BusinessAccount { get; set; }
}
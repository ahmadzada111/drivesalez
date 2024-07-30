using System.Text.Json.Serialization;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }

    public string Number { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public ApplicationUser ApplicationUser{ get; set; }
}
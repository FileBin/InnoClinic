using MongoDB.Bson;

namespace OfficesAPI.Domain.Models;

public class Office {
    public ObjectId Id { get; set; }
    public required Address Address { get; set; }
    public required string RegistryPhoneNumber { get; set; }
    public Guid? PhotoId { get; set; }
    public required bool IsActive { get; set; }
}

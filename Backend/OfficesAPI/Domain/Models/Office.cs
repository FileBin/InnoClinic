using MongoDB.Bson;

namespace OfficesAPI.Domain.Models;

public class Office {
    [Column("id")]
    public ObjectId Id { get; set; }

    [Column("address")]
    public required Address Address { get; set; }

    [Column("registry_phone_number"), MaxLength(32)]
    public required string RegistryPhoneNumber { get; set; }
    
    [Column("photo_id")]
    public Guid? PhotoId { get; set; }

    [Column("is_active")]
    public required bool IsActive { get; set; }
}

namespace OfficesAPI.Domain.Models;

public class Address {
    [Column("city"), MaxLength(128)]
    public required string City { get; init; }

    [Column("street"), MaxLength(128)]
    public required string Street { get; init; }

    [Column("house_number"), MaxLength(16)]
    public required string HouseNumber { get; init; }
    
    [Column("office_number"), MaxLength(16)]
    public string? OfficeNumber { get; init; }
}

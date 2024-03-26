namespace OfficesAPI.Domain.Models;

public class Address {
    [Column("city"), MaxLength(128)]
    public required string City { get; set; }

    [Column("street"), MaxLength(128)]
    public required string Street { get; set; }

    [Column("house_number"), MaxLength(16)]
    public required string HouseNumber { get; set; }
    
    [Column("office_number"), MaxLength(16)]
    public string? OfficeNumber { get; set; }
}

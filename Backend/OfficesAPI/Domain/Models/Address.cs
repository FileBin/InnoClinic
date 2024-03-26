namespace OfficesAPI.Domain.Models;

public class Address {
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string HouseNumber { get; set; }
    public string? OfficeNumber { get; set; }
}

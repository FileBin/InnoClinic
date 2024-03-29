namespace OfficesAPI.Application.Contracts.Models;

public record AddressDto {
    [Required]
    [StringLength(128)]
    public required string City { get; init; }

    [Required]
    [StringLength(128)]
    public required string Street { get; init; }

    [Required]
    [StringLength(16)]
    public required string HouseNumber { get; init; }

    [StringLength(16)]
    public string? OfficeNumber { get; init; }
}

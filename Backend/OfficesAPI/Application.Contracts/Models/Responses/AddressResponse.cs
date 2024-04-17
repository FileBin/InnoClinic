namespace OfficesAPI.Application.Contracts.Models.Responses;

public record AddressResponse {
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

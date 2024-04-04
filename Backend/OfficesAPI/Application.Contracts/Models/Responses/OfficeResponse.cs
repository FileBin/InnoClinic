namespace OfficesAPI.Application.Contracts.Models.Responses;

public record OfficeResponse {
    [Required]
    public Guid Id { get; init; }

    [Required]
    public required AddressResponse Address { get; init; }

    [Required]
    [StringLength(32)]
    [DataType(DataType.PhoneNumber)]
    public required string RegistryPhoneNumber { get; init; }

    [Required]
    public required bool IsActive { get; init; }
}


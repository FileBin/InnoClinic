namespace OfficesAPI.Application.Contracts.Models;

public record OfficeCreateDto {
    [Required]
    public required AddressDto Address { get; init; }

    [Required]
    [StringLength(32)]
    [DataType(DataType.PhoneNumber)]
    public required string RegistryPhoneNumber { get; init; }

    [Required]
    public required bool IsActive { get; init; }
}


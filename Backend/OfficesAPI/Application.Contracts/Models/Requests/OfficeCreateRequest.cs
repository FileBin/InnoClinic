namespace OfficesAPI.Application.Contracts.Models.Requests;

public record OfficeCreateRequest
{
    [Required]
    public required AddressDto Address { get; init; }

    [Required]
    [StringLength(32)]
    [DataType(DataType.PhoneNumber)]
    public required string RegistryPhoneNumber { get; init; }

    [Required]
    public required bool IsActive { get; init; }
}


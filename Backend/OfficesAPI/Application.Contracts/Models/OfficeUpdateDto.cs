namespace OfficesAPI.Application.Contracts.Models;

public record OfficeUpdateDto {
    public AddressDto? Address { get; init; }

    [StringLength(32)]
    [DataType(DataType.PhoneNumber)]
    public string? RegistryPhoneNumber { get; init; }

    public bool? IsActive { get; init; }
}


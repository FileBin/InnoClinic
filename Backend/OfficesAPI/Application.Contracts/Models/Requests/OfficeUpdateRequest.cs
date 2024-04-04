namespace OfficesAPI.Application.Contracts.Models.Requests;

public record OfficeUpdateRequest {
    public AddressRequest? Address { get; init; }

    [StringLength(32)]
    [DataType(DataType.PhoneNumber)]
    public string? RegistryPhoneNumber { get; init; }

    public bool? IsActive { get; init; }
}


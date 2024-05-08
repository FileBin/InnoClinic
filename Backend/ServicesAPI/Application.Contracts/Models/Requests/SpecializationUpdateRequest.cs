namespace ServicesAPI.Application.Contracts.Models.Requests;

public record SpecializationUpdateRequest {

    public string? Name { get; init; }

    public bool? IsActive { get; init; }
}

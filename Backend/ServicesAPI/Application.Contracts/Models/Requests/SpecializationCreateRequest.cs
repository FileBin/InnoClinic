namespace ServicesAPI.Application.Contracts.Models.Requests;

public record SpecializationCreateRequest {

    public required string Name { get; init; }

    public required bool IsActive { get; init; }
}

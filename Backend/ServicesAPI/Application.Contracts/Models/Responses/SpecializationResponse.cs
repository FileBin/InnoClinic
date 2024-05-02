namespace ServicesAPI.Application.Contracts.Models.Responses;

public record SpecializationResponse {
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required bool IsActive { get; init; }
}



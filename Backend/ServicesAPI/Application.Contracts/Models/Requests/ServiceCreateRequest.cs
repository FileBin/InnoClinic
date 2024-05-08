namespace ServicesAPI.Application.Contracts.Models.Requests;

public record ServiceCreateRequest {
    public Guid SpecializationId { get; init; }

    public Guid CategoryId { get; init; }

    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public required bool IsActive { get; init; }
}

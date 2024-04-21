namespace ServicesAPI.Application.Contracts.Models.Requests;

public record ServiceUpdateRequest {
    public Guid? SpecializationId { get; init; }

    public Guid? CategoryId { get; init; }

    public string? Name { get; init; }

    public decimal? Price { get; init; }

    public bool? IsActive { get; init; }
}

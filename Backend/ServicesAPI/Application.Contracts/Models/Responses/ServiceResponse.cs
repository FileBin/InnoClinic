namespace ServicesAPI.Application.Contracts.Models.Responses;

public record ServiceResponse {
    public Guid Id { get; init; }

    public required SpecializationResponse Specialization { get; init; }

    public required ServiceCategoryResponse Category { get; set; }

    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public required bool IsActive { get; init; }
}

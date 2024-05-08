namespace ServicesAPI.Application.Contracts.Models.Responses;

public record ServiceCategoryResponse {
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required int TimeSlotSize { get; init; }
}

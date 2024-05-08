namespace ServicesAPI.Application.Contracts.Models.Requests;

public record ServiceCategoryCreateRequest {
    public required string Name { get; init; }

    public required int TimeSlotSize { get; init; }
}

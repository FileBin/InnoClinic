namespace ServicesAPI.Application.Contracts.Models.Requests;

public record ServiceCategoryUpdateRequest {
    public string? Name { get; init; }

    public int? TimeSlotSize { get; init; }
}

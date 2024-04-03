using OfficesAPI.Application.Contracts.Models.Requests;

namespace OfficesAPI.Application.Contracts.Models.Responses;

public record OfficeResponse : OfficeCreateRequest {
    [Required]
    public Guid Id { get; init; }
}


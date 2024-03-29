namespace OfficesAPI.Application.Contracts.Models;

public record OfficeDto : OfficeCreateDto {
    [Required]
    public Guid Id { get; init; }
}


namespace AppointmentsAPI.Application.Contracts.Models.Requests;

public record AppointmentResultUpdateRequest {

    public Guid? AppointmentId { get; init; }

    public required string? Complaints { get; set; }

    public required string? Conclusion { get; init; }

    public required string? Recommendations { get; init; }
}

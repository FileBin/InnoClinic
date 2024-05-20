namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands;

public record CreateAppointmentResultCommand : IRequest<Guid> {
    public Guid AppointmentId { get; init; }

    public string? Complaints { get; init; }
    public string? Conclusion { get; init; }
    public string? Recommendations { get; init; }
}

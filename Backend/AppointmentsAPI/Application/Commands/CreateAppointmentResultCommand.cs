using AppointmentsAPI.Application.Contracts.Commands;

namespace AppointmentsAPI.Application.Commands;

public record CreateAppointmentResultCommand : ICommand<Guid> {
    public Guid AppointmentId { get; init; }

    public string? Complaints { get; init; }
    public string? Conclusion { get; init; }
    public string? Recommendations { get; init; }
}

using AppointmentsAPI.Application.Contracts.Commands;

namespace AppointmentsAPI.Application.Commands;

public record CancelAppointmentCommand : ICommand {
    public required Guid AppointmentId { get; init; }
}

using AppointmentsAPI.Application.Contracts.Commands;

namespace AppointmentsAPI.Application.Commands;

public record ApproveAppointmentCommand : ICommand {
    public required Guid AppointmentId { get; init; }
}
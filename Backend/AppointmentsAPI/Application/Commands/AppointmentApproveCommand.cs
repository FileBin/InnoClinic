using AppointmentsAPI.Application.Contracts.Commands;

namespace AppointmentsAPI.Application.Commands;

public record AppointmentApproveCommand : ICommand {
    public required Guid AppointmentId { get; init; }
}
using AppointmentsAPI.Application.Contracts.Commands;

namespace AppointmentsAPI.Application.Commands;

public record AppointmentDeleteCommand : ICommand {
    public required Guid AppointmentId { get; init; }
}

using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Requests;

namespace AppointmentsAPI.Application.Commands;

public record AppointmentUpdateCommand : AppointmentUpdateRequest, ICommand {
    public required Guid AppointmentId { get; init; }
}

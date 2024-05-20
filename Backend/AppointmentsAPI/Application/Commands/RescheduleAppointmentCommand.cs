using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Requests;

namespace AppointmentsAPI.Application.Commands;

public record RescheduleAppointmentCommand : RescheduleAppointmentRequest, ICommand {
    public required Guid AppointmentId { get; init; }
}

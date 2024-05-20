using AppointmentsAPI.Application.Contracts.Models.Requests;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands;

public record RescheduleAppointmentCommand : TimeSlotRequest, IRequest {
    public required Guid AppointmentId { get; init; }
}

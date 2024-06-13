namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;

public record RescheduleAppointmentRequest : TimeSlotRequest {
    public required Guid AppointmentId { get; init; }
}

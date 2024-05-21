namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;

public record CancelAppointmentRequest {
    public required Guid AppointmentId { get; init; }
}

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;

public record ApproveAppointmentRequest {
    public required Guid AppointmentId { get; init; }
}
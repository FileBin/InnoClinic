namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands;

public record CancelAppointmentCommand : IRequest {
    public required Guid AppointmentId { get; init; }
}

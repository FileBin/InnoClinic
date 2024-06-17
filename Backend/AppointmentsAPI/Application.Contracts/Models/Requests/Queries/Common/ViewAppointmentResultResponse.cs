namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Common;

public record ViewAppointmentResultRequest {
    public Guid AppointmentId { get; init; }
}

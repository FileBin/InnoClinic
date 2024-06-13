namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Common;

public record ViewAppointmentScheduleRequest {
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(DateTime.Now);
}

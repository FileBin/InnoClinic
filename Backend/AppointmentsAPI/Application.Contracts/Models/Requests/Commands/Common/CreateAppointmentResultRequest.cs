namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;

public record CreateAppointmentResultRequest {
    public string? Complaints { get; init; }
    public string? Conclusion { get; init; }
    public string? Recommendations { get; init; }
}

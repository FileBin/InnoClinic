namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;

public record CreateAppointmentRequest {

    public Guid DoctorId { get; init; }

    public Guid ServiceId { get; init; }

    public required DateOnly Date { get; init; }

    public required TimeOnly BeginTime { get; set; }

    public required TimeOnly EndTime { get; set; }
}

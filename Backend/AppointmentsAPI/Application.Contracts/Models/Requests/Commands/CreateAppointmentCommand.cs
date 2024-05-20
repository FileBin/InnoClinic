using AppointmentsAPI.Application.Contracts.Models.Requests;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands;

public record CreateAppointmentCommand : IRequest<Guid> {
    public Guid PatientId { get; init; }

    public Guid DoctorId { get; init; }

    public Guid ServiceId { get; init; }

    public required DateOnly Date { get; init; }

    public required TimeOnly BeginTime { get; set; }

    public required TimeOnly EndTime { get; set; }
}

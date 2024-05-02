namespace AppointmentsAPI.Application.Contracts.Models.Responses;

public record AppointmentResponse {
    public Guid Id { get; init; }

    public Guid PatientId { get; init; }

    public Guid DoctorId { get; init; }

    public Guid ServiceId { get; init; }

    public required DateOnly Date { get; init; }

    public required TimeOnly Time { get; init; }

    public required bool IsApproved { get; init; }
}


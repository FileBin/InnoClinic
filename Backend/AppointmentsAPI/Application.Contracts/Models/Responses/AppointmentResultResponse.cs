namespace AppointmentsAPI.Application.Contracts.Models.Responses;

public record AppointmentResultResponse {
    public Guid Id { get; init; }

    public Guid AppointmentId { get; init; }

#region Additional Information

    public required DateOnly Date { get; init; }

    public required string PatientFullName { get; init; }

    public required DateOnly PatientDateOfBirth { get; init; }

    public required string DoctorFullName { get; init; }

    public required string DoctorSpecializationName { get; init; }

    public required string ServiceName { get; init; }

#endregion

    public required string Complaints { get; init; }

    public required string Conclusion { get; init; }

    public required string Recommendations { get; init; }
}

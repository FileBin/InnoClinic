using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries;

public record ViewAppointmentsListQuery : IRequest<IEnumerable<IEnumerable<AppointmentResponse>>> {
    public DateOnly? Date { get; init; }
    public string? DoctorFullName { get; init; }
    public bool? IsApproved { get; init; }
    public Guid? OfficeId { get; init; }
}

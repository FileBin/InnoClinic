using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Models;


namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;

public record ViewAppointmentsListQuery : PageDesc, IRequest<IEnumerable<AppointmentResponse>> {
    public DateOnly? Date { get; init; }
    public string? DoctorFullName { get; init; }
    public bool? IsApproved { get; init; }
    public Guid? OfficeId { get; init; }
}

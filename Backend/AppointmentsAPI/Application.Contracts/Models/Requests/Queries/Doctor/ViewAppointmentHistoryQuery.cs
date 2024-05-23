using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Domain.Models;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;

public record ViewAppointmentHistoryQuery : PageDesc, IDoctorRequest, IRequest<IEnumerable<IEnumerable<AppointmentResponse>>> {
    public Guid PatientId { get; init; }
    public required IUserDescriptor DoctorDescriptor { get; init; }
}

using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Domain.Models;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;

public record ViewAppointmentHistoryQuery : PageDesc, IPatientRequest, IRequest<IEnumerable<IEnumerable<AppointmentResponse>>> {
    public required IUserDescriptor PatientDescriptor { get; init; }
}

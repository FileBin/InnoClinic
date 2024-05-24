using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;

public record ViewAppointmentResultQuery: Common.ViewAppointmentResultRequest, IPatientRequest, IRequest<AppointmentResultResponse> {
    public required IUserDescriptor PatientDescriptor {get; init;}
}

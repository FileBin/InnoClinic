using AppointmentsAPI.Application.Contracts.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;

public record CancelAppointmentCommand : Common.CancelAppointmentRequest, IPatientRequest, IRequest {
    public required IUserDescriptor PatientDescriptor { get; init; }
}

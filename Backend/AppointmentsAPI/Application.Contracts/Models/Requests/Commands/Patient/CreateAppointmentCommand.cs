using AppointmentsAPI.Application.Contracts.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;

public record CreateAppointmentCommand : Common.CreateAppointmentRequest, IPatientRequest, IRequest<Guid> {
    public required IUserDescriptor PatientDescriptor { get; init; }
}

using AppointmentsAPI.Application.Contracts.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;

public record RescheduleAppointmentCommand : Common.RescheduleAppointmentRequest, IPatientRequest, IRequest {
    public required IUserDescriptor PatientDescriptor { get; init; }
}


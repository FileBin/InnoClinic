using AppointmentsAPI.Application.Contracts.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;

public record UpdateAppointmentResultCommand : Common.UpdateAppointmentResultRequest, IDoctorRequest, IRequest<Guid> {
    public required IUserDescriptor DoctorDescriptor { get; init; }
}
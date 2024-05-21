using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;

public record ViewAppointmentScheduleQuery : Common.ViewAppointmentScheduleRequest, IDoctorRequest, IRequest<IEnumerable<AppointmentResponse>> {
    public required IUserDescriptor DoctorDescriptor { get; init; }
}

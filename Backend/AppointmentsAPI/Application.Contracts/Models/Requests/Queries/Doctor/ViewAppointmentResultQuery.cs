using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;

public record ViewAppointmentResultQuery: Common.ViewAppointmentResultResponse, IDoctorRequest, IRequest<AppointmentResultResponse> {
    public required IUserDescriptor DoctorDescriptor { get; init; }
}

using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries;

public record ViewAppointmentScheduleQuery : IRequest<IEnumerable<AppointmentResponse>> {
    public required IUserDescriptor DoctorDescriptor { get; init; }
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(DateTime.Now);
}

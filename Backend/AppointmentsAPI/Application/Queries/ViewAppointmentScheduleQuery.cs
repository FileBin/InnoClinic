using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Queries;

public record ViewAppointmentScheduleQuery : ICommand<IEnumerable<AppointmentResponse>> {
    public required IUserDescriptor DoctorDescriptor { get; init; }
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(DateTime.Now);
}

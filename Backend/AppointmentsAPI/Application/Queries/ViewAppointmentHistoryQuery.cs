using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Queries;

public record ViewAppointmentHistoryQuery : ICommand<IEnumerable<IEnumerable<AppointmentResponse>>> {
    public required IUserDescriptor PatientDescriptor { get; init; }
    public required IPageDesc PageDesc { get; init; }
}

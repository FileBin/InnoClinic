using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries;

public record ViewAppointmentHistoryQuery : IRequest<IEnumerable<IEnumerable<AppointmentResponse>>> {
    public required IUserDescriptor PatientDescriptor { get; init; }
    public required IPageDesc PageDesc { get; init; }
}

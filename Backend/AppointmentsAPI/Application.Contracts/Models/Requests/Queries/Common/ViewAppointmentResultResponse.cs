using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Common;

public record ViewAppointmentResultRequest {
    public Guid AppointmentId { get; init; }
}

using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;

public record GetAppointmentQuery : IRequest<AppointmentResponse> {
    public Guid AppointmentId { get; init; }
}

using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;

public record AppointmentGetQuery : IRequest<AppointmentResponse> {
    public Guid AppointmentId { get; init; }
}

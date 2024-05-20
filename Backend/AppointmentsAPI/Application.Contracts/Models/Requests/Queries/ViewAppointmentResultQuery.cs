using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries;

public record ViewAppointmentResultQuery : IRequest<AppointmentResultResponse> {
    public Guid AppointmentId { get; set; }
}

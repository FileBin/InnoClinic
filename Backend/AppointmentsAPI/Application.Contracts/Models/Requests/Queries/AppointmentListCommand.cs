using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries;

public record AppointmentListQuery : IRequest<IEnumerable<AppointmentResponse>> { }

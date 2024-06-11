using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;

public record ViewAppointmentResultQuery: Common.ViewAppointmentResultRequest, IRequest<AppointmentResultResponse> {}

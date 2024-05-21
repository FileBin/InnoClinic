using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Models;

namespace AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;

public record ViewAppointmentHistoryQuery : PageDesc, IRequest<IEnumerable<IEnumerable<AppointmentResponse>>> {}

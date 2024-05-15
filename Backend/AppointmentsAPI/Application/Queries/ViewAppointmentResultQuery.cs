using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Queries;

public class ViewAppointmentResultQuery : ICommand<AppointmentResultResponse> {
    public Guid AppointmentId { get; set; }
}

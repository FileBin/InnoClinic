using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class ViewAppointmentListHandler
    : IRequestHandler<ViewAppointmentHistoryQuery, IEnumerable<IEnumerable<AppointmentResponse>>> {
    public Task<IEnumerable<IEnumerable<AppointmentResponse>>> Handle(ViewAppointmentHistoryQuery request, CancellationToken cancellationToken) {
        if(request.)
    }
}

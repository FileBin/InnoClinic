using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class ViewAppointmentHistoryHandler(IRepository<Appointment> repository)
    : IRequestHandler<ViewAppointmentHistoryQuery, IEnumerable<IEnumerable<AppointmentResponse>>> {
    public async Task<IEnumerable<IEnumerable<AppointmentResponse>>> Handle(ViewAppointmentHistoryQuery request, CancellationToken cancellationToken) {
        var appointmentsGrps = await repository.GetAll()
            .Where(a => a.PatientId.ToString() == request.PatientDescriptor.Id)
            .OrderByDescending(a => a.Date)
            .GroupBy(a => a.Date)
            .Select(grp => grp.OrderBy(a => a.BeginTime))
            .ToListAsync(cancellationToken);

        return appointmentsGrps
            .Select(grp => grp.Adapt<List<AppointmentResponse>>())
            .ToList();
    }
}

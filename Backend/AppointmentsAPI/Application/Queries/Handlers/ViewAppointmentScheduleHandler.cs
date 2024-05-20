using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class ViewAppointmentScheduleHandler(IRepository<Appointment> repository)
    : IRequestHandler<ViewAppointmentScheduleQuery, IEnumerable<AppointmentResponse>> {
    public async Task<IEnumerable<AppointmentResponse>> Handle(ViewAppointmentScheduleQuery request, CancellationToken cancellationToken) {
        var appointments = await repository.GetAll()
            .Where(a => a.DoctorId.ToString() == request.DoctorDescriptor.Id)
            .Where(a => a.Date == request.Date)
            .OrderBy(a => a.BeginTime)
            .ToListAsync(cancellationToken);

        return appointments.Adapt<List<AppointmentResponse>>();
    }
}

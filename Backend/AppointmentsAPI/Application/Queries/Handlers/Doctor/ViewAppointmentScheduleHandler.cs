using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Application.Helpers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers.Doctor;

using DoctorEntity = Domain.Models.Doctor;

public class ViewAppointmentScheduleHandler(IRepository<Appointment> appointmentRepo, IRepository<DoctorEntity> doctorRepo)
    : IRequestHandler<ViewAppointmentScheduleQuery, IEnumerable<AppointmentResponse>> {
    public async Task<IEnumerable<AppointmentResponse>> Handle(ViewAppointmentScheduleQuery request, CancellationToken cancellationToken) {
        var doctorId = await request.GetDoctorIdAsync(doctorRepo, cancellationToken);

        var appointments = await appointmentRepo.GetAll()
            .Where(a => a.DoctorId == doctorId)
            .Where(a => a.Date == request.Date)
            .OrderBy(a => a.BeginTime)
            .ToListAsync(cancellationToken);

        return appointments.Adapt<List<AppointmentResponse>>();
    }
}

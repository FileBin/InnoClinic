using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Application.Helpers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace AppointmentsAPI.Application.Queries.Handlers.Doctor;

using DoctorEntity = Domain.Models.Doctor;


public class ViewAppointmentHistoryHandler(IRepository<Appointment> appointmentRepo, IRepository<DoctorEntity> doctorRepo)
    : IRequestHandler<ViewAppointmentHistoryQuery, IEnumerable<IEnumerable<AppointmentResponse>>> {
    public async Task<IEnumerable<IEnumerable<AppointmentResponse>>> Handle(ViewAppointmentHistoryQuery request, CancellationToken cancellationToken) {
        var doctorId = await request.GetDoctorIdAsync(doctorRepo, cancellationToken);
        
        var appointmentsGroups = await appointmentRepo.GetAll()
            .Where(a => a.PatientId == request.PatientId)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.Date)
            .GroupBy(a => a.Date)
            .Select(grp => grp.OrderBy(a => a.BeginTime))
            .ToListAsync(cancellationToken);

        return appointmentsGroups
            .Select(grp => grp.Adapt<List<AppointmentResponse>>())
            .ToList();
    }
}

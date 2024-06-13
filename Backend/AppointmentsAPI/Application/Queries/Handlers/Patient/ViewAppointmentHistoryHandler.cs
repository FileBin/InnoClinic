using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Application.Helpers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace AppointmentsAPI.Application.Queries.Handlers.Patient;

using PatientEntity = Domain.Models.Patient;


public class ViewAppointmentHistoryHandler(IRepository<Appointment> appointmentRepository, IRepository<PatientEntity> patientRepository)
    : IRequestHandler<ViewAppointmentHistoryQuery, IEnumerable<IEnumerable<AppointmentResponse>>> {
    public async Task<IEnumerable<IEnumerable<AppointmentResponse>>> Handle(ViewAppointmentHistoryQuery request, CancellationToken cancellationToken) {
        var patientId = await request.GetPatientIdAsync(patientRepository, cancellationToken);
        
        var appointmentsGroups = await appointmentRepository.GetAll()
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.Date)
            .GroupBy(a => a.Date)
            .Select(grp => grp.OrderBy(a => a.BeginTime))
            .ToListAsync(cancellationToken);

        return appointmentsGroups
            .Select(grp => grp.Adapt<List<AppointmentResponse>>())
            .ToList();
    }
}

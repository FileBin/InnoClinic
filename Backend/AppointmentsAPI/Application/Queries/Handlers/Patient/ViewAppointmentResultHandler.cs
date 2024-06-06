using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers.Patient;

using PatientEntity = Domain.Models.Patient;

public class ViewAppointmentResultHandler(IRepository<Appointment> appointmentRepo, IRepository<PatientEntity> patientRepo)
    : IRequestHandler<ViewAppointmentResultQuery, AppointmentResultResponse> {
    public async Task<AppointmentResultResponse> Handle(ViewAppointmentResultQuery request, CancellationToken cancellationToken) {
        var appointmentEntity = await appointmentRepo
            .GetAll()
            .Include(x => x.AppointmentResult)
            .Include(x => x.PatientProfile)
            .Include(x => x.DoctorProfile)
            .Include(x => x.Service)
            .FirstOrDefaultAsync(x=> x.Id == request.AppointmentId, cancellationToken)
            ?? throw new NotFoundException($"Appointment with id {request.AppointmentId} was not found");
        
        await appointmentEntity.ValidateAppointmentPatientAccessAsync(request, patientRepo, cancellationToken);

        var appointmentResultEntity = appointmentEntity.AppointmentResult
            ?? throw new BadRequestException("This appointment hasn't result yet!");

        var patientProfile = appointmentEntity.PatientProfile;
        var doctorProfile = appointmentEntity.DoctorProfile;
        var service = appointmentEntity.Service;

        return appointmentResultEntity.Adapt<AppointmentResultResponse>() with {
            Date = appointmentEntity.Date,

            PatientFullName = $"{patientProfile.FirstName} {patientProfile.LastName} {patientProfile.MiddleName}",
            PatientDateOfBirth = patientProfile.DateOfBirth,

            DoctorFullName = $"{doctorProfile.FirstName} {doctorProfile.LastName} {doctorProfile.MiddleName}",
            DoctorSpecializationName = service.Specialization.Name,

            ServiceName = service.Name,
        };
    }
}

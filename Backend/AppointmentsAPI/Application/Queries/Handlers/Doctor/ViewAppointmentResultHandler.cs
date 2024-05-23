using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Queries.Handlers.Doctor;

public class ViewAppointmentResultHandler(IRepository<Appointment> appointmentRepo)
    : IRequestHandler<ViewAppointmentResultQuery, AppointmentResultResponse> {
    public async Task<AppointmentResultResponse> Handle(ViewAppointmentResultQuery request, CancellationToken cancellationToken) {
        var appointmentEntity = await appointmentRepo.GetByIdOrThrow(request.AppointmentId, cancellationToken);

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

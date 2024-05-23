using AppointmentsAPI.Application.Contracts.Abstraction;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers.Patient;

using PatientEntity = Domain.Models.Patient;

using DoctorEntity = Domain.Models.Doctor;

public static class Extensions {

    public static async Task ValidateAppointmentEditAccessAsync(this Appointment appointmentEntity, IDoctorRequest request, IRepository<DoctorEntity> repository, CancellationToken cancellationToken) {
        if (request.DoctorDescriptor.IsAdmin()) return;

        appointmentEntity.ValidateAppointmentEditAccess(await request.GetDoctorIdAsync(repository, cancellationToken));
    }

    public static async Task ValidateAppointmentPatientAccessAsync(this Appointment appointmentEntity, IPatientRequest request, IRepository<PatientEntity> repository, CancellationToken cancellationToken) {
        appointmentEntity.ValidateAppointmentPatientAccess(await request.GetPatientIdAsync(repository, cancellationToken));
    }

    public static void ValidateAppointmentEditAccess(this Appointment appointmentEntity, Guid doctorId) {
        if (appointmentEntity.DoctorId != doctorId) {
            throw new NotFoundException($"Appointment with id {appointmentEntity.Id} was not found");
        }
    }

    public static void ValidateAppointmentPatientAccess(this Appointment appointmentEntity, Guid patientId) {
        if (appointmentEntity.PatientId != patientId) {
            throw new NotFoundException($"Appointment with id {appointmentEntity.Id} was not found");
        }
    }

    public static async Task<DoctorEntity> GetDoctorAsync(this IDoctorRequest doctorRequest, IRepository<DoctorEntity> repository, CancellationToken cancellationToken) {
        var userId = doctorRequest.DoctorDescriptor.Id;

        return await repository.GetAll().SingleOrDefaultAsync(d => d.UserId.ToString() == userId, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"The doctor with user_id={userId} was not found");
    }

    public static async Task<Guid> GetDoctorIdAsync(this IDoctorRequest doctorRequest, IRepository<DoctorEntity> repository, CancellationToken cancellationToken) {
        var patient = await doctorRequest.GetDoctorAsync(repository, cancellationToken);
        return patient.Id;
    }

    public static async Task<PatientEntity> GetPatientAsync(this IPatientRequest patientRequest, IRepository<PatientEntity> repository, CancellationToken cancellationToken) {
        var userId = patientRequest.PatientDescriptor.Id;

        return await repository.GetAll().SingleOrDefaultAsync(p => p.UserId.ToString() == userId, cancellationToken)
            ?? throw new NotFoundException($"The patient with user_id={userId} was not found");
    }

    public static async Task<Guid> GetPatientIdAsync(this IPatientRequest patientRequest, IRepository<PatientEntity> repository, CancellationToken cancellationToken) {
        var patient = await patientRequest.GetPatientAsync(repository, cancellationToken);
        return patient.Id;
    }
}
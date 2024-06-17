using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Application.Helpers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers.Patient;

using PatientEntity = Domain.Models.Patient;

public class CreateAppointmentHandler(IRepository<Appointment> appointmentRepo, IRepository<PatientEntity> patientRepo, IUnitOfWork unitOfWork) : IRequestHandler<CreateAppointmentCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken) {
        var appointment = request.Adapt<Appointment>();
        appointment.PatientId = await request.GetPatientIdAsync(patientRepo, cancellationToken);

        appointmentRepo.Create(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return appointment.Id;
    }
}

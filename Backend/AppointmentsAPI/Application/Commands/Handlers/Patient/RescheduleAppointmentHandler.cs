using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Application.Helpers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers.Patient;

using PatientEntity = Domain.Models.Patient;

public class RescheduleAppointmentHandler(IRepository<Appointment> appointmentRepo, IRepository<PatientEntity> patientRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<RescheduleAppointmentCommand> {
    public async Task Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken) {
        var appointment = await appointmentRepo.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        await appointment.ValidateAppointmentPatientAccessAsync(request, patientRepo, cancellationToken);

        request.Adapt(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

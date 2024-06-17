using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Application.Helpers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers.Patient;

using PatientEntity = Domain.Models.Patient;

public class CancelAppointmentHandler(IRepository<Appointment> appointmentRepo, IRepository<PatientEntity> patientRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<CancelAppointmentCommand> {
    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await appointmentRepo.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        
        await appointmentEntity.ValidateAppointmentPatientAccessAsync(request, patientRepo, cancellationToken);
        
        await appointmentRepo.DeleteByIdAsync(request.AppointmentId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

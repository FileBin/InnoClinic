using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Application.Queries.Handlers.Patient;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class CreateAppointmentResultHandler(IRepository<Appointment> appointmentRepo, IRepository<Doctor> doctorRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAppointmentResultCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await appointmentRepo.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        await appointmentEntity.ValidateAppointmentEditAccessAsync(request, doctorRepo, cancellationToken);

        appointmentEntity.AppointmentResult = new AppointmentResult {
            Complaints = request.Complaints ?? "",
            Conclusion = request.Conclusion ?? "",
            Recommendations = request.Recommendations ?? "",
            
            IsFinished = false,
        };

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointmentEntity.AppointmentResult.Id;
    }
}

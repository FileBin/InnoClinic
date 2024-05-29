using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Application.Queries.Handlers.Patient;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers.Doctor;

using Doctor = Domain.Models.Doctor;

internal class CreateAppointmentResultHandler(IRepository<Appointment> appointmentRepo, IRepository<Doctor> doctorRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAppointmentResultCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await appointmentRepo.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        await appointmentEntity.ValidateAppointmentEditAccessAsync(request, doctorRepo, cancellationToken);

        appointmentEntity.AppointmentResult = new AppointmentResult {
            Complaints = request.Complaints ?? string.Empty,
            Conclusion = request.Conclusion ?? string.Empty,
            Recommendations = request.Recommendations ?? string.Empty,
            
            IsFinished = false,
        };

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointmentEntity.AppointmentResult.Id;
    }
}

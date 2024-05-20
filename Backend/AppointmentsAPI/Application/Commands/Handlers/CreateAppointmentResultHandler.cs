using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class CreateAppointmentResultHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAppointmentResultCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        appointmentEntity.AppointmentResult = new AppointmentResult {
            Complaints = request.Complaints ?? "",
            Conclusion = request.Conclusion ?? "",
            Recommendations = request.Recommendations ?? "",
        };

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointmentEntity.AppointmentResult.Id;
    }
}

using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class RescheduleAppointmentHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork)
    : ICommandHandler<RescheduleAppointmentCommand> {
    public async Task Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken) {
        var appointment = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        request.Adapt(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

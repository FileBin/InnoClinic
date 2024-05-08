using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class AppointmentUpdateHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : ICommandHandler<AppointmentUpdateCommand> {
    public async Task Handle(AppointmentUpdateCommand request, CancellationToken cancellationToken) {
        var appointment = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        request.Adapt(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

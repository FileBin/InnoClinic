using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class AppointmentApproveHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : ICommandHandler<AppointmentApproveCommand> {
    public async Task Handle(AppointmentApproveCommand request, CancellationToken cancellationToken) {
        var appointment = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        appointment.IsApproved = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

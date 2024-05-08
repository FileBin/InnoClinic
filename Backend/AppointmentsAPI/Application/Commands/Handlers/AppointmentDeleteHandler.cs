using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class AppointmentDeleteHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : ICommandHandler<AppointmentDeleteCommand> {
    public async Task Handle(AppointmentDeleteCommand request, CancellationToken cancellationToken) {
        await repository.DeleteByIdAsync(request.AppointmentId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

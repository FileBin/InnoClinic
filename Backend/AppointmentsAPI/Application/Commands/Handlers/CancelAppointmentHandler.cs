using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class CancelAppointmentHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : IRequestHandler<CancelAppointmentCommand> {
    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken) {
        await repository.DeleteByIdAsync(request.AppointmentId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

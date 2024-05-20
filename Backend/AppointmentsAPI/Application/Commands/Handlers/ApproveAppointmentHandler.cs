using AppointmentsAPI.Application.Contracts.Models.Requests.Commands;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class ApproveAppointmentHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : IRequestHandler<ApproveAppointmentCommand> {
    public async Task Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken) {
        var appointment = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        appointment.IsApproved = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

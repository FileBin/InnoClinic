using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application.Commands.Handlers.Receptionist;

public class CancelAppointmentHandler(IRepository<Appointment> appointmentRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<CancelAppointmentCommand> {
    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken) {
        await appointmentRepo.EnsureExistsAsync(request.AppointmentId, cancellationToken);
        
        await appointmentRepo.DeleteByIdAsync(request.AppointmentId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

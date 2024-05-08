using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class AppointmentCreateHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : ICommandHandler<AppointmentCreateCommand, Guid> {
    public async Task<Guid> Handle(AppointmentCreateCommand request, CancellationToken cancellationToken) {
        var appointment = request.Adapt<Appointment>();
        repository.Create(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return appointment.Id;
    }
}

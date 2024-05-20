using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class CreateAppointmentHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork) : ICommandHandler<CreateAppointmentCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken) {
        var appointment = request.Adapt<Appointment>();
        repository.Create(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return appointment.Id;
    }
}

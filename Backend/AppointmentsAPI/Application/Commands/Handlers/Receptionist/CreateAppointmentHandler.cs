using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers.Receptionist;

public class CreateAppointmentHandler(IRepository<Appointment> appointmentRepo, IUnitOfWork unitOfWork) : IRequestHandler<CreateAppointmentCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken) {
        var appointment = request.Adapt<Appointment>();
        
        appointmentRepo.Create(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return appointment.Id;
    }
}

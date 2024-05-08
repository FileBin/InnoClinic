using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class AppointmentGetHandler(IRepository<Appointment> repository) : ICommandHandler<AppointmentGetCommand, AppointmentResponse>
{
    public async Task<AppointmentResponse> Handle(AppointmentGetCommand request, CancellationToken cancellationToken)
    {
        var appointment = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        return appointment.Adapt<AppointmentResponse>();
    }
}


using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class AppointmentGetHandler(IRepository<Appointment> repository) : ICommandHandler<AppointmentGetQuery, AppointmentResponse>
{
    public async Task<AppointmentResponse> Handle(AppointmentGetQuery request, CancellationToken cancellationToken)
    {
        var appointment = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        return appointment.Adapt<AppointmentResponse>();
    }
}


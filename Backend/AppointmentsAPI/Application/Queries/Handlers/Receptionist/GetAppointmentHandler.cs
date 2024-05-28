using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Queries.Handlers.Receptionist;

public class GetAppointmentHandler(IRepository<Appointment> repository)
    : IRequestHandler<GetAppointmentQuery, AppointmentResponse> {
    public async Task<AppointmentResponse> Handle(GetAppointmentQuery request, CancellationToken cancellationToken) {
        var appointmentEntity = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);
        
        return appointmentEntity.Adapt<AppointmentResponse>();
    }
}
using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class AppointmentListHandler(IRepository<Appointment> repository)
    : ICommandHandler<AppointmentListCommand, IEnumerable<AppointmentResponse>> {
    public async Task<IEnumerable<AppointmentResponse>> Handle(AppointmentListCommand request, CancellationToken cancellationToken) {
        var appointments = await repository.GetAll().ToListAsync(cancellationToken);
        return appointments.Select(x => x.Adapt<AppointmentResponse>()).ToList();
    }
}

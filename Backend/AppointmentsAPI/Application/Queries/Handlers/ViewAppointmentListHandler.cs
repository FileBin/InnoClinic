using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Queries.Handlers;

public class ViewAppointmentListHandler(IRepository<Appointment> repository)
    : IRequestHandler<ViewAppointmentsListQuery, IEnumerable<AppointmentResponse>> {
    public async Task<IEnumerable<AppointmentResponse>> Handle(ViewAppointmentsListQuery request, CancellationToken cancellationToken) {
        var date = request.Date;
        var doctorFullName = request.DoctorFullName?.Trim()?.ToLower();
        var officeId = request.OfficeId;
        var IsApproved = request.IsApproved;

        var appointmentsQuery = repository.GetAll();

        if (date.HasValue) {
            appointmentsQuery = appointmentsQuery
                .Where(a => a.Date == date.Value);
        }

        if (doctorFullName is not null) {
            appointmentsQuery = appointmentsQuery
                .Where(a =>
                    $"{a.DoctorProfile.FirstName.ToLower()} {a.DoctorProfile.LastName.ToLower()} {a.DoctorProfile.MiddleName.ToLower()}" == doctorFullName);
        }

        if (officeId.HasValue) {
            appointmentsQuery = appointmentsQuery
                .Where(a => a.DoctorProfile.OfficeId == officeId.Value);
        }

        if (IsApproved.HasValue) {
            appointmentsQuery = appointmentsQuery
                .Where(a => a.IsApproved == IsApproved.Value);
        }

        var result = await appointmentsQuery
            .Paginate(request)
            .ToListAsync(cancellationToken);

        return result.Adapt<List<AppointmentResponse>>();
    }
}

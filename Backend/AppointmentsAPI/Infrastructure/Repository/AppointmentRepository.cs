using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class AppointmentsRepository(AppointmentsDbContext dbContext) : CrudRepositoryBase<Appointment> {
    protected override DbSet<Appointment> GetDbSet() => dbContext.Appointments;
}

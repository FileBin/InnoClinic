using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class AppointmentResultsRepository(AppointmentsDbContext dbContext) : CrudRepositoryBase<AppointmentResult> {
    protected override DbSet<AppointmentResult> GetDbSet() => dbContext.AppointmentResults;
}

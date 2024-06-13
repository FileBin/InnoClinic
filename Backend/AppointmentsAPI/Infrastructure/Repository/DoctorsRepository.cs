using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class DoctorsRepository(AppointmentsDbContext dbContext) : CrudRepositoryBase<Doctor> {
    protected override DbSet<Doctor> GetDbSet() => dbContext.Doctors;
}

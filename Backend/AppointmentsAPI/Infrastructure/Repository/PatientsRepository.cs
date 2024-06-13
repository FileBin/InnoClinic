using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class PatientsRepository(AppointmentsDbContext dbContext) : CrudRepositoryBase<Patient> {
    protected override DbSet<Patient> GetDbSet() => dbContext.Patients;
}

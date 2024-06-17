using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class SpecializationsRepository(AppointmentsDbContext dbContext) : CrudRepositoryBase<Specialization> {
    protected override DbSet<Specialization> GetDbSet() => dbContext.Specializations;
}
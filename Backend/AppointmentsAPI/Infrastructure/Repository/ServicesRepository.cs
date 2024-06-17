using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using AppointmentsAPI.Infrastructure.Database;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class ServicesRepository(AppointmentsDbContext dbContext) : CrudRepositoryBase<Service> {
    protected override DbSet<Service> GetDbSet() => dbContext.Services;
}

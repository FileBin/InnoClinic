using AppointmentsAPI.Infrastructure.Database;
using InnoClinic.Shared.Misc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Infrastructure.Repository;

internal class UnitOfWork(AppointmentsDbContext dbContext) : UnitOfWorkBase {
    public override DbContext GetDbContext() => dbContext;
}

using OfficesAPI.Infrastructure.Database;
using InnoClinic.Shared.Misc;
using Microsoft.EntityFrameworkCore;

namespace OfficesAPI.Infrastructure.Repository;

internal class UnitOfWork(OfficeDbContext dbContext) : UnitOfWorkBase {
    public override DbContext GetDbContext() => dbContext;
}

using Microsoft.EntityFrameworkCore;
using OfficesAPI.Domain.Models;
using OfficesAPI.Infrastructure.Database;
using InnoClinic.Shared.Misc.Repository;

namespace OfficesAPI.Infrastructure.Repository;

internal class OfficeRepository(OfficeDbContext dbContext) : CrudRepositoryBase<Office> {
    protected override DbSet<Office> GetDbSet() => dbContext.Offices;
}

using Microsoft.EntityFrameworkCore;
using OfficesAPI.Domain.Models;
using OfficesAPI.Infrastructure.Database;
using InnoClinic.Shared.Domain.Abstractions;

namespace OfficesAPI.Infrastructure.Repository;

internal class OfficeRepository(OfficeDbContext dbContext) : IRepository<Office> {
    public void Create(Office entity) {
        dbContext.Offices.Add(entity);
    }

    public void Delete(Office entity) {
        dbContext.Offices.Remove(entity);
    }

    public IQueryable<Office> GetAll() {
        return dbContext.Offices.AsNoTracking();
    }

    public async Task<Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await GetAll().SingleOrDefaultAsync(office => office.Id == id, cancellationToken);
    }

    public void Update(Office entity) {
        dbContext.Offices.Entry(entity).State = EntityState.Modified;
    }
}

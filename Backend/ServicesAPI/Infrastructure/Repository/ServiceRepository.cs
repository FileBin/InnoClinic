using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Domain;
using Shared.Misc;

namespace ServicesAPI.Infrastructure.Repository;

internal class ServiceRepository(ServicesDbContext dbContext) : IRepository<Service> {
    public void Create(Service entity) {
        dbContext.Services.Add(entity);
    }

    public void Delete(Service entity) {
        dbContext.Services.Remove(entity);
    }

    public IQueryable<Service> GetAll() {
        return dbContext.Services.AsNoTracking();
    }

    public async Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await GetAll().SingleOrDefaultAsync(office => office.Id == id, cancellationToken);
    }

    public void Update(Service entity) {
        dbContext.Services.Entry(entity).State = EntityState.Modified;
    }
}

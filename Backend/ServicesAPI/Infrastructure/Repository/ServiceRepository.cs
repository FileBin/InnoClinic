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

    public async Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await dbContext.Services
            .AsNoTracking()
            .SingleOrDefaultAsync(office => office.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Service>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        return await dbContext.Services
            .AsNoTracking()
            .Paginate(pageDesc)
            .ToListAsync(cancellationToken);
    }

    public void Update(Service entity) {
        dbContext.Services.Entry(entity).State = EntityState.Modified;
    }
}

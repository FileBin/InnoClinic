using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure.Repository;

internal class ServiceCategoryRepository(ServicesDbContext dbContext) : IRepository<ServiceCategory> {
    public void Create(ServiceCategory entity) {
        dbContext.ServiceCategories.Add(entity);
    }

    public void Delete(ServiceCategory entity) {
        dbContext.ServiceCategories.Remove(entity);
    }

    public IQueryable<ServiceCategory> GetAll() {
        return dbContext.ServiceCategories.AsNoTracking();
    }

    public async Task<ServiceCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await GetAll().SingleOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public void Update(ServiceCategory entity) {
        dbContext.ServiceCategories.Entry(entity).State = EntityState.Modified;
    }
}

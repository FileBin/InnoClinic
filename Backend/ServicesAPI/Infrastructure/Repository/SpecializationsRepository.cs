using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure.Repository;

internal class SpecializationsRepository(ServicesDbContext dbContext) : IRepository<Specialization> {
    public void Create(Specialization entity) {
        dbContext.Specializations.Add(entity);
    }

    public void Delete(Specialization entity) {
        dbContext.Specializations.Remove(entity);
    }

    public IQueryable<Specialization> GetAll() {
        return dbContext.Specializations.AsNoTracking();
    }

    public async Task<Specialization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await GetAll().SingleOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public void Update(Specialization entity) {
        dbContext.Specializations.Entry(entity).State = EntityState.Modified;
    }
}

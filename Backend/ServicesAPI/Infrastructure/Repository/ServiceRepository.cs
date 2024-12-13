using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure.Repository;

internal class ServiceRepository(ServicesDbContext dbContext) : CrudRepositoryBase<Service>, IRepository<Service> {
    protected override DbSet<Service> GetDbSet() => dbContext.Services;

    public override IQueryable<Service> GetAll() {
        return GetDbSet().Include(x => x.Specialization).Include(x => x.Category).AsNoTracking();
    }
}

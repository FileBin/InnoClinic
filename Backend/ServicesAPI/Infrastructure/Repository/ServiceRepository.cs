using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure.Repository;

internal class ServiceRepository(ServicesDbContext dbContext) : CrudRepositoryBase<Service> {
    protected override DbSet<Service> GetDbSet() => dbContext.Services;
}

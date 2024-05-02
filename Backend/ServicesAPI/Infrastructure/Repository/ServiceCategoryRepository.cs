using InnoClinic.Shared.Misc.Repository;
using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure.Repository;

internal class ServiceCategoryRepository(ServicesDbContext dbContext) : CrudRepositoryBase<ServiceCategory> {
    protected override DbSet<ServiceCategory> GetDbSet() => dbContext.ServiceCategories;
}

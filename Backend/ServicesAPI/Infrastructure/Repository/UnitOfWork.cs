using InnoClinic.Shared.Misc;

namespace ServicesAPI.Infrastructure.Repository;

internal class UnitOfWork(ServicesDbContext dbContext) : UnitOfWorkBase {
    public override DbContext GetDbContext() => dbContext;
}

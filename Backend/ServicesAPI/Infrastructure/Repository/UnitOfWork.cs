using InnoClinic.Shared.Domain.Abstractions;

namespace ServicesAPI.Infrastructure.Repository;

internal class UnitOfWork(ServicesDbContext dbContext) : IUnitOfWork {
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}

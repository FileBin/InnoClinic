using OfficesAPI.Infrastructure.Database;
using InnoClinic.Shared.Domain.Abstractions;

namespace OfficesAPI.Infrastructure.Repository;

internal class UnitOfWork(OfficeDbContext dbContext) : IUnitOfWork {
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

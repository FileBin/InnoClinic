using OfficesAPI.Infrastructure.Database;
using Shared.Domain.Abstractions;

namespace OfficesAPI.Infrastructure.Repository;

public class UnitOfWork(OfficeDbContext dbContext) : IUnitOfWork {
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

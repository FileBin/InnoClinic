﻿using Microsoft.EntityFrameworkCore;
using OfficesAPI.Domain.Models;
using OfficesAPI.Infrastructure.Database;
using Shared.Domain.Abstractions;
using Shared.Misc;

namespace OfficesAPI.Infrastructure.Repository;

public class OfficeRepository(OfficeDbContext dbContext) : IRepository<Office> {
    public void Create(Office entity) {
        dbContext.Offices.Add(entity);
    }

    public void Delete(Office entity) {
        dbContext.Offices.Remove(entity);
    }

    public async Task<Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await dbContext.Offices
            .SingleOrDefaultAsync(office => office.Id == id);
    }

    public async Task<IEnumerable<Office>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var skip = (pageDesc.PageNumber - 1) * pageDesc.PageSize;
        var take = pageDesc.PageSize;

        return await dbContext.Offices
            .Paginate(pageDesc)
            .ToListAsync(cancellationToken);
    }

    public void Update(Office entity) {
        dbContext.Offices.Entry(entity).State = EntityState.Modified;
    }
}

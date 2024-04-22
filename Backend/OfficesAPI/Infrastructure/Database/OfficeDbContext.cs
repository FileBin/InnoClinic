using Microsoft.EntityFrameworkCore;
using OfficesAPI.Domain.Models;

namespace OfficesAPI.Infrastructure.Database;

internal class OfficeDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<Office> Offices => Set<Office>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
    }
}

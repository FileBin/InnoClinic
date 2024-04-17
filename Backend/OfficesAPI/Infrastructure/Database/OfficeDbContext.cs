using Microsoft.EntityFrameworkCore;
using OfficesAPI.Domain.Models;

namespace OfficesAPI.Infrastructure.Database;

internal class OfficeDbContext : DbContext {
    public OfficeDbContext(DbContextOptions options) : base(options) {
    }

    public DbSet<Office> Offices => Set<Office>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
    }
}
